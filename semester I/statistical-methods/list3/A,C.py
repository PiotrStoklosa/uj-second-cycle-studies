import random

import matplotlib.pyplot as plt
import numpy as np


def x(n, mu):
    return -np.log(n) / mu


def randomise_x_function(mu):
    return x(random.random(), mu)


def simulate(mu_A, mu_S):
    simulation_A = [0]
    while simulation_A[-1] < 100:
        simulation_A.append(simulation_A[-1] + randomise_x_function(mu_A))

    simulation_S1 = [0]
    for i in range(len(simulation_A)):
        simulation_S1.append(randomise_x_function(mu_S))

    queue = []

    task_x = []
    task_y_in_queue = []
    task_y_finished = []

    finished_task_x = []
    finished_task_y_in_queue = []
    finished_task_y_finished = []

    amount_of_task_finished = 0
    start_time_of_last_task = 0

    for i in range(1, len(simulation_A)):
        while len(queue) and start_time_of_last_task + simulation_S1[queue[0]] < simulation_A[i]:
            amount_of_task_finished += 1
            finished_task_y_finished.append(amount_of_task_finished)
            finished_task = queue.pop(0)
            finished_task_x.append(start_time_of_last_task + simulation_S1[finished_task])
            finished_task_y_in_queue.append(len(queue))
            start_time_of_last_task = start_time_of_last_task + simulation_S1[finished_task]
        if len(queue) == 0:
            start_time_of_last_task = simulation_A[i]
        task_x.append(simulation_A[i])
        task_y_in_queue.append(len(queue))
        task_y_finished.append(amount_of_task_finished)
        queue.append(i)

    while queue:
        amount_of_task_finished += 1
        finished_task_y_finished.append(amount_of_task_finished)
        finished_task = queue.pop(0)
        finished_task_x.append(start_time_of_last_task + simulation_S1[finished_task])
        finished_task_y_in_queue.append(len(queue))
        start_time_of_last_task = start_time_of_last_task + simulation_S1[finished_task]

    plt.title('The amount of task in the queue (time), mu_A=' + str(mu_A) + ', mu_S=' + str(mu_S))
    plt.scatter(task_x, task_y_in_queue, label='New task')
    plt.scatter(finished_task_x, finished_task_y_in_queue, label='Task finished')
    plt.xlabel('Time')
    plt.ylabel('In queue')
    plt.yticks(range(int(min(task_y_in_queue)), int(max(task_y_in_queue)) + 1))
    plt.legend()
    plt.show()

    plt.title('The amount of task finished (time), mu_A=' + str(mu_A) + ', mu_S=' + str(mu_S))
    plt.scatter(task_x, task_y_finished, label='New task')
    plt.scatter(finished_task_x, finished_task_y_finished, label='Task finished')
    plt.xlabel('Time')
    plt.ylabel('In queue')
    plt.yticks(range(int(min(finished_task_y_finished)), int(max(finished_task_y_finished)) + 1))
    plt.legend()
    plt.show()


mu_A1 = 1 / 20
mu_S1 = 1 / 20

mu_A2 = 1 / 20
mu_S2 = 1 / 15

mu_A3 = 1 / 20
mu_S3 = 1 / 100

simulate(mu_A1, mu_S1)
simulate(mu_A2, mu_S2)
simulate(mu_A3, mu_S3)

# Answers to questions:
# Co oznaczają zależności czasowe:
# (mu_a - mu_s)t
# Odchylenie ilości zadań w kolejce od 0
# (mu_a / mu_s)t
# Wskaźnik jak szybko rośnie liczba zadań wraz z czasem

mu1_difference = mu_A1 - mu_S1
mu2_difference = mu_A2 - mu_S2
mu3_difference = mu_A3 - mu_S3

mu1_ratio = mu_A1 / mu_S1
mu2_ratio = mu_A2 / mu_S2
mu3_ratio = mu_A3 / mu_S3

x = [i for i in range(1, 500)]
y_1_diff = [(mu1_difference * i) for i in range(1, 500)]
y_2_diff = [(mu2_difference * i) for i in range(1, 500)]
y_3_diff = [(mu3_difference * i) for i in range(1, 500)]
y_1_ratio = [(mu1_ratio * i) for i in range(1, 500)]
y_2_ratio = [(mu2_ratio * i) for i in range(1, 500)]
y_3_ratio = [(mu3_ratio * i) for i in range(1, 500)]

plt.plot(x, y_1_diff)
plt.title('The difference between mu_A=' + str(mu_A1) + ' and mu_S=' + str(mu_S1) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 - 1/20')
plt.show()

plt.plot(x, y_2_diff)
plt.title('The difference between mu_A=' + str(mu_A2) + ' and mu_S=' + str(mu_S2) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 - 1/15')
plt.show()

plt.plot(x, y_3_diff)
plt.title('The difference between mu_A=' + str(mu_A3) + ' and mu_S=' + str(mu_S3) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 - 1/100')
plt.show()

plt.plot(x, y_1_ratio)
plt.title('The ratio between mu_A=' + str(mu_A1) + ' and mu_S=' + str(mu_S1) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 / 1/20')
plt.show()

plt.plot(x, y_2_ratio)
plt.title('The ratio between mu_A=' + str(mu_A2) + ' and mu_S=' + str(mu_S2) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 / 1/15')
plt.show()

plt.plot(x, y_3_ratio)
plt.title('The ratio between mu_A=' + str(mu_A3) + ' and mu_S=' + str(mu_S3) + ' (time)')
plt.xlabel('Time')
plt.ylabel('1/20 / 1/100')
plt.show()
