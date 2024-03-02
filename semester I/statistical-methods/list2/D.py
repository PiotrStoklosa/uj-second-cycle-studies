import random

from matplotlib import pyplot as plt


def simulate(start, n):
    visits = [0] * 100
    visits[start] = 1
    trackExp = []
    trackExp.append(visits[:])
    users_online = start
    for i in range(n):
        users_online_before_changes = users_online
        for j in range(100):
            draw = random.random()
            if j < users_online_before_changes:
                if draw < 1 - (0.008 * users_online_before_changes + 0.1):
                    users_online -= 1
            else:
                if draw < 0.2:
                    users_online += 1
        visits[users_online] += 1
        trackExp.append(visits[:])
    return visits, trackExp


simulation_size = 1000000
visits, trackExp = simulate(50, simulation_size)
for index, visit in enumerate(visits):
    print(index + 1, round(visit / simulation_size, 5))
for i in [9, 19, 29, 39, 49]:
    plt.plot([round(track[i] / (index + 1), 5) for index, track in enumerate(trackExp)], label="EXP_" + str(i + 1))
    plt.xlabel("n")
    plt.legend()
    plt.show()
