import matplotlib.pyplot as plt

import math
import random


def generate_normal_distribution_numbers(size):
    values = []

    for _ in range(size // 2):
        u1 = random.random()
        u2 = random.random()

        z1 = math.sqrt(-2 * math.log(u1)) * math.cos(2 * math.pi * u2)
        z2 = math.sqrt(-2 * math.log(u1)) * math.sin(2 * math.pi * u2)

        values.append(z1)
        values.append(z2)

    if size % 2 == 1:
        u = random.random()
        z = math.sqrt(-2 * math.log(u)) * math.cos(2 * math.pi * random.random())
        values.append(z)

    return values


plt.hist(generate_normal_distribution_numbers(1000000), bins=10, density=True)
plt.show()
