import random

import matplotlib.pyplot as plt
import numpy as np
from scipy.stats import poisson

f = np.e


def x(t):
    return -np.log(t)


def randomise_x_function():
    return x(random.random())


simulation_charts = 1000
simulation_steps_per_charts = 200

simulations = []

for i in range(simulation_charts):
    simulation = [0]
    for step in range(simulation_steps_per_charts):
        simulation.append(simulation[-1] + randomise_x_function())
    simulations.append(simulation)

print(simulations[0])

t_1 = []
t_20 = []
t_90 = []


for simulation in simulations:
    for i in range(len(simulation)):
        if simulation[i] > 1:
            t_1.append(i - 1)
            break

for simulation in simulations:
    for i in range(len(simulation)):
        if simulation[i] > 20:
            t_20.append(i - 1)
            break

for simulation in simulations:
    for i in range(len(simulation)):
        if simulation[i] > 90:
            t_90.append(i - 1)
            break


def compare(data, t):
    poisson_dist = poisson(mu=t)
    x_t = np.arange(min(data), max(data) + 1, 1)
    plt.hist(data, x_t, color='blue', edgecolor='black', density=True)
    plt.plot(x_t, poisson_dist.pmf(x_t), 'ro-', lw=2)
    plt.show()


compare(t_1, 1)
compare(t_20, 20)
compare(t_90, 90)
