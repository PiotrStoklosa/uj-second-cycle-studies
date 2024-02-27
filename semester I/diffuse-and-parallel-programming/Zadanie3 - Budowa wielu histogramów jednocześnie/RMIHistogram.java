import javax.naming.InitialContext;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;

public class RMIHistogram extends UnicastRemoteObject implements RemoteHistogram, Binder {

    private final Map<Integer, int[]> histograms = Collections.synchronizedMap(new HashMap<>());

    private final AtomicInteger histogramIDGenerator = new AtomicInteger();

    protected RMIHistogram() throws RemoteException {
        super(1099);
    }


    @Override
    public void bind(String serviceName) {
        try {
            RMIHistogram rmiHistogram = new RMIHistogram();
            InitialContext context = new InitialContext();
            context.bind("rmi:" + serviceName, rmiHistogram);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    @Override
    public int createHistogram(int bins) throws RemoteException {
        int histogramID = histogramIDGenerator.getAndIncrement();
        histograms.put(histogramID, new int[bins]);
        return histogramID;
    }

    @Override
    public void addToHistogram(int histogramID, int value) throws RemoteException {
        synchronized(histograms.get(histogramID)) {
            int oldValue = histograms.get(histogramID)[value];
            int newValue = oldValue + 1;
            histograms.get(histogramID)[value] = newValue;
        }
    }

    @Override
    public int[] getHistogram(int histogramID) throws RemoteException {
        return histograms.get(histogramID);
    }
}
