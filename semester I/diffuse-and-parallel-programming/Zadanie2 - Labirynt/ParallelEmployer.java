import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ParallelEmployer implements Employer {

    private OrderInterface orderInterface;
    private final Map<Integer, Location> pendingOrders = Collections.synchronizedMap(new HashMap<>());
    private Location exitLocation;
    private final Object lock = new Object();

    final List<Location> orderedLocations = new ArrayList<>();

    @Override
    public void setOrderInterface(OrderInterface order) {
        orderInterface = order;
    }

    @Override
    public Location findExit(Location startLocation, List<Direction> allowedDirections) {

        orderInterface.setResultListener(
                result -> {
                    Location currentLocation = pendingOrders.remove(result.orderID());
                    if (result.type() == LocationType.EXIT) {
                        synchronized (lock) {
                            exitLocation = currentLocation;
                            lock.notify();
                        }
                    } else {
                        for (Direction direction : result.allowedDirections()) {

                            Location newLocation = direction.step(currentLocation);

                            synchronized (orderedLocations) {
                                if (!orderedLocations.contains(newLocation) && exitLocation == null) {

                                    orderedLocations.add(newLocation);
                                    pendingOrders.put(orderInterface.order(newLocation), newLocation);

                                }
                            }
                        }
                    }
                });

        synchronized (lock) {

            orderedLocations.add(startLocation);

            for (Direction direction : allowedDirections) {

                Location newLocation = direction.step(startLocation);

                synchronized (orderedLocations) {
                    orderedLocations.add(newLocation);
                }

                pendingOrders.put(orderInterface.order(newLocation), newLocation);
            }

            try {
                lock.wait();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }

            return exitLocation;
        }
    }
}
