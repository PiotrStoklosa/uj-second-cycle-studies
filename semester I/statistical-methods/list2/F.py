import timeit
from random import random

import numpy as np
from scipy.stats import norm, cauchy

mean = 0
std_dev = 1
steps = 10000


def rejection_sampling():
    random_x = random() * 12 - 6
    random_y = random() * 0.5
    return random_x, random_y


def draw(function_for_randomise, limit=0):
    points_in_norm = []
    if limit == 0:
        for i in range(steps):
            random_x, random_y = function_for_randomise()
            if norm.pdf(random_x, loc=mean, scale=std_dev) >= random_y:
                points_in_norm.append([random_x, random_y])
        return points_in_norm
    else:
        while len(points_in_norm) < 10000:
            random_x, random_y = function_for_randomise()
            if norm.pdf(random_x, loc=mean, scale=std_dev) >= random_y:
                points_in_norm.append([random_x, random_y])
        return points_in_norm


print("Generating 10e4 numbers")
start_time = timeit.default_timer()
points = draw(rejection_sampling)
end_time = timeit.default_timer()

print(len(points) * 100 / steps, '%')

elapsed_time = end_time - start_time

print("It tooks", elapsed_time, "seconds")

print("Generating 10e4 numbers in normal distribution")
start_time = timeit.default_timer()

points = draw(rejection_sampling, steps)

end_time = timeit.default_timer()
elapsed_time = end_time - start_time

print(len(points) * 100 / steps, '%')
print("It tooks", elapsed_time, "seconds")


def inverse_cumulative_distribution(x):
    return np.tan(np.pi * (x - 0.5))


def rejection_sampling_cauchy():
    ran = random()
    random_x = inverse_cumulative_distribution(ran)
    random_y = cauchy.pdf(random_x, loc=mean, scale=std_dev)
    return random_x, random_y


for i in range(50):
    rejection_sampling_cauchy()

print("Generating 10e4 numbers using Cuachy")

start_time = timeit.default_timer()
points = draw(rejection_sampling_cauchy)
end_time = timeit.default_timer()

print(len(points) * 100 / steps, '%')

elapsed_time = end_time - start_time

print("It tooks", elapsed_time, "seconds")

print("Generating 10e4 numbers in normal distribution using Cuachy")
start_time = timeit.default_timer()

points = draw(rejection_sampling_cauchy, steps)

end_time = timeit.default_timer()
elapsed_time = end_time - start_time

print(len(points) * 100 / steps, '%')
print("It tooks", elapsed_time, "seconds")
