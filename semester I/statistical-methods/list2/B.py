import random

import numpy as np
from matplotlib import pyplot as plt

P = np.array([[0.64, 0.32, 0.04], [0.4, 0.5, 0.1], [0.25, 0.5, 0.25]])
solution = [0.51020408, 0.40816327, 0.08163265]  # calculated in list2/A.py


def simulate(start, n, concurrence):
    P_after = P
    end = 0
    visits = [0, 0, 0]
    trackExp = []
    trackDot = []
    visits[start] = 1
    trackExp.append(visits[:])
    trackDot.append([P[0, 0], P[0, 1], P[0, 2]])
    users_online = start
    for i in range(n):
        end += 1
        if (visits[0] / end - P_after[0, 0] < concurrence and
                visits[1] / end - P_after[0, 1] < concurrence and
                visits[2] / end - P_after[0, 2] < concurrence):
            return visits, trackExp, trackDot, end
        P_before = P_after
        P_after = np.dot(P_before, P)

        draw = random.random()
        users_online_before_changes = users_online
        for j in range(2):
            draw = random.random()
            if j < users_online_before_changes:
                if draw < 0.5:
                    users_online -= 1
            else:
                if draw < 0.2:
                    users_online += 1
        visits[users_online] += 1
        trackExp.append(visits[:])
        trackDot.append([P_after[0, 0], P_after[0, 1], P_after[0, 2]])
    return visits, trackExp, trackDot, end


steps = 10000
concurrence_limit = 0.0001

for start_node in range(3):
    visited_nodes, track_exp_visited_nodes, track_dot_visited_nodes, _ = simulate(0, steps, 0)
    print("N=10^4, start z węzła", start_node)
    print([visited_nodes[0] / steps, visited_nodes[1] / steps, visited_nodes[2] / steps])
    print()
    for j in range(3):
        plt.plot([(track0[j] / (index + 1)) for index, track0 in enumerate(track_exp_visited_nodes)],
                 label="EXP_" + str(j + 1))
        plt.plot([trackDoti[j] for trackDoti in track_dot_visited_nodes], label="P")
        plt.plot([solution[j]] * steps, label="Pi_" + str(j + 1))
        plt.xlabel("n - początek w " + str(start_node + 1))
        plt.legend()
        plt.show()

    visitsC, trackExpC, trackDotC, endC = simulate(0, 10000000, concurrence_limit)
    if endC == 10000000:
        print("Zbieżność do", concurrence_limit, "nie nastąpiła, tablica exp:",
              [visitsC[0] / endC, visitsC[1] / endC, visitsC[2] / endC])
        print("P:", trackDotC[-1])
    else:
        print("Zbieżność exp do P z marginesem błędu", concurrence_limit, "nastąpiła po", endC, "krokach")
        print("Exp:", [visitsC[0] / endC, visitsC[1] / endC, visitsC[2] / endC])
        print("P:", trackDotC[-1])
    print()
