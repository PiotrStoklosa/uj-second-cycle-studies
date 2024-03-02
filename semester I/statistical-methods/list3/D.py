import random

import matplotlib.pyplot as plt
import numpy as np


def x(n, mu):
    return -np.log(n) / mu


def randomise_x_function(mu):
    return x(random.random(), mu)


def simulate(mu_A, mu_S):
    simulation_A = [0]
    while simulation_A[-1] < 1000:
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
    return time_in_system / len(simulation_A), mean_tasks_in_system / simulation_A[-1]


def simulate_2(mu_A, mu_S):
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
    return time_in_system / len(simulation_A), mean_tasks_in_system / simulation_A[-1]


mu_A1 = [(i / 100) for i in range(10, 101)]
mu_S1 = 1 / 15

mean_wait_time1 = []
mean_tasks1 = []

for mu_A in mu_A1:
    mean_wait_time_for_specific_mu = 0
    mean_tasks_for_specific_mu = 0
    for j in range(30):
        mean_wait_time, mean_tasks = simulate(mu_A, mu_S1)
        mean_tasks_for_specific_mu += mean_tasks
        mean_wait_time_for_specific_mu += mean_wait_time

    mean_wait_time1.append(mean_wait_time_for_specific_mu / 30)
    mean_tasks1.append(mean_tasks_for_specific_mu / 30)

plt.plot(mu_A1, mean_tasks1)
plt.title('Oczekiwana liczba zadań w systemie dla mu_S = 1/15 oraz t = 1000')
plt.xlabel('mu_A')
plt.ylabel('Liczba zadań')
plt.show()

plt.plot(mu_A1, mean_wait_time1)
plt.title('Oczekiwany czas na ukończenie zadania dla mu_S = 1/15 oraz t = 1000')
plt.xlabel('mu_A')
plt.ylabel('Czas ukończenia zadania')
plt.show()

mu_S2 = [(i / 100) for i in range(10, 101)]
mu_A2 = 1 / 15

mean_wait_time2 = []
mean_tasks2 = []

for mu_S in mu_S2:
    mean_wait_time_for_specific_mu = 0
    mean_tasks_for_specific_mu = 0
    for j in range(30):
        mean_wait_time, mean_tasks = simulate(mu_A2, mu_S)
        mean_tasks_for_specific_mu += mean_tasks
        mean_wait_time_for_specific_mu += mean_wait_time

    mean_wait_time2.append(mean_wait_time_for_specific_mu / 30)
    mean_tasks2.append(mean_tasks_for_specific_mu / 30)

plt.plot(mu_S2, mean_tasks2)
plt.title('Oczekiwana liczba zadań w systemie dla mu_A = 1/15 oraz t = 1000')
plt.xlabel('mu_A')
plt.ylabel('Liczba zadań')
plt.show()

plt.plot(mu_S2, mean_wait_time2)
plt.title('Oczekiwany czas na ukończenie zadania dla mu_A = 1/15 oraz t = 1000')
plt.xlabel('mu_A')
plt.ylabel('Czas ukończenia zadania')
plt.show()

ratio = [(i / 100) for i in range(1, 101)]
ratio2 = [i for i in range(1, 100)]

mean_wait_time3A = []
mean_tasks3A = []

for mu_A in ratio:
    mean_wait_time_for_specific_mu = 0
    mean_tasks_for_specific_mu = 0
    for j in range(30):
        mean_wait_time, mean_tasks = simulate(mu_A / 10, 1 / 10)
        mean_tasks_for_specific_mu += mean_tasks
        mean_wait_time_for_specific_mu += mean_wait_time

    mean_wait_time3A.append(mean_wait_time_for_specific_mu / 30)
    mean_tasks3A.append(mean_tasks_for_specific_mu / 30)

plt.plot(ratio, mean_tasks3A)
plt.title('Oczekiwana liczba zadań w systemie dla t = 1000')
plt.xlabel('mu_A / mu_S')
plt.ylabel('Liczba zadań')
plt.show()

plt.plot(ratio, mean_wait_time3A)
plt.title('Oczekiwany czas na ukończenie zadania dla t = 1000')
plt.xlabel('mu_A / mu_S')
plt.ylabel('Czas ukończenia zadania')
plt.show()

mean_wait_time3B = []
mean_tasks3B = []

for mu_S in ratio2:
    mean_wait_time_for_specific_mu = 0
    mean_tasks_for_specific_mu = 0
    for j in range(30):
        mean_wait_time, mean_tasks = simulate(1 / 10, (100 - mu_S) / 10)
        mean_tasks_for_specific_mu += mean_tasks
        mean_wait_time_for_specific_mu += mean_wait_time

    mean_wait_time3B.append(mean_wait_time_for_specific_mu / 30)
    mean_tasks3B.append(mean_tasks_for_specific_mu / 30)

plt.plot(ratio2, mean_tasks3B)
plt.title('Oczekiwana liczba zadań w systemie dla t = 1000')
plt.xlabel('mu_A / mu_S')
plt.ylabel('Liczba zadań')
plt.show()

plt.plot(ratio2, mean_wait_time3B)
plt.title('Oczekiwany czas na ukończenie zadania dla t = 1000')
plt.xlabel('mu_A / mu_S')
plt.ylabel('Czas ukończenia zadania')
plt.show()
