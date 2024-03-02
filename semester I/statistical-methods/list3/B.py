import random

import numpy as np


def x(n, mu):
    return -np.log(n) / mu


def randomise_x_function(mu):
    return x(random.random(), mu)


def simulate(mu_A, mu_S):
    simulation_A = [0]
    while simulation_A[-1] < 10000:
        simulation_A.append(simulation_A[-1] + randomise_x_function(mu_A))

    simulation_S = [0]
    for i in range(len(simulation_A)):
        simulation_S.append(randomise_x_function(mu_S))

    queue = []

    time_in_system = 0
    mean_tasks_in_system = 0

    start_time_of_last_task = 0

    for i in range(1, len(simulation_A)):
        while len(queue) and start_time_of_last_task + simulation_S[queue[0]] < simulation_A[i]:
            time_in_system_of_one_task = start_time_of_last_task + simulation_S[queue[0]] - simulation_A[queue[0]]
            time_in_system += time_in_system_of_one_task
            mean_tasks_in_system += time_in_system_of_one_task
            finished_task = queue.pop(0)
            start_time_of_last_task = start_time_of_last_task + simulation_S[finished_task]
        if len(queue) == 0:
            start_time_of_last_task = simulation_A[i]
        queue.append(i)
    while queue:
        not_finished_task = queue.pop(0)
        time_in_system_of_one_task = simulation_A[-1] - simulation_A[not_finished_task]
        time_in_system += time_in_system_of_one_task
        mean_tasks_in_system += time_in_system_of_one_task
    return time_in_system / len(simulation_A), mu_A, mean_tasks_in_system / simulation_A[-1]

div = 0

for j in range(1000):
    E_r, mu_A, E_k = simulate(1 / 20, 1 / 15)
    print("E_r * mu_A= " + str(E_r * mu_A))
    print("E_k = " + str(E_k))
    print("Difference = " + str(E_r * mu_A - E_k))
    div += E_r * mu_A - E_k

print("Avarage diff= " + str(div / 1000))