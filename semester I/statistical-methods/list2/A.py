import numpy as np
from matplotlib import pyplot as plt


def calculate_max_difference(matrix_a, matrix_b):
    difference_matrix = matrix_a - matrix_b
    return difference_matrix.max()


P = np.array([[0.64, 0.32, 0.04], [0.4, 0.5, 0.1], [0.25, 0.5, 0.25]])
print("[P]^(10):")
print(np.linalg.matrix_power(P, 10))
print()
print("[P]^(100):")
print(np.linalg.matrix_power(P, 100))
print()
print("[P]^(1000):")
print(np.linalg.matrix_power(P, 1000))
print()

plot_1_1 = []
plot_1_2 = []
plot_1_3 = []
plot_2_1 = []
plot_2_2 = []
plot_2_3 = []
plot_3_1 = []
plot_3_2 = []
plot_3_3 = []


def insert_to_plots(matrix):
    plot_1_1.append(matrix[0, 0])
    plot_2_1.append(matrix[1, 0])
    plot_3_1.append(matrix[2, 0])

    plot_1_2.append(matrix[0, 1])
    plot_2_2.append(matrix[1, 1])
    plot_3_2.append(matrix[2, 1])

    plot_1_3.append(matrix[0, 2])
    plot_2_3.append(matrix[1, 2])
    plot_3_3.append(matrix[2, 2])


insert_to_plots(P)

P_before = P
P_after = np.dot(P_before, P)
concurrence_parameter = 1e-5

insert_to_plots(P_after)

power = 2

while calculate_max_difference(P_before, P_after) > concurrence_parameter:
    power += 1
    P_before = P_after
    P_after = np.dot(P_before, P)

    insert_to_plots(P_after)

print("Zbieżność osiągnięta dla n =", power)

system_of_equations = np.transpose(P) - np.array([[1, 0, 0], [0, 1, 0], [0, 0, 1]])

system_of_equations = np.vstack((system_of_equations[:-1], np.array([1, 1, 1])))

solution = np.linalg.solve(system_of_equations, np.array([0, 0, 1]))

plt.plot(plot_1_1, label="1_1")
plt.plot(plot_2_1, label="2_1")
plt.plot(plot_3_1, label="3_1")
plt.plot([solution[0]] * power, label="Pi_1")
plt.xlabel("n")
plt.legend()

plt.show()

plt.plot(plot_1_2, label="1_2")
plt.plot(plot_2_2, label="2_2")
plt.plot(plot_3_2, label="3_2")
plt.plot([solution[1]] * power, label="Pi_2")
plt.xlabel("n")
plt.legend()

plt.show()

plt.plot(plot_1_3, label="1_3")
plt.plot(plot_2_3, label="2_3")
plt.plot(plot_3_3, label="3_3")
plt.plot([solution[2]] * power, label="Pi_3")
plt.xlabel("n")
plt.legend()

plt.show()

print(P[1, 0])
print(P)