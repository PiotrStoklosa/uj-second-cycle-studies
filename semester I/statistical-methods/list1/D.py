import random
import matplotlib.pyplot as plt
import numpy as np


def player_ruin_duration(number_of_games, probability_of_winning_a, capital_a, capital_b):
    durations = []
    for i in range(0, number_of_games):
        time = 0
        capital_a_game = capital_a
        capital_b_game = capital_b
        while capital_a_game != 0 and capital_b_game != 0:
            time += 1
            if random.random() < probability_of_winning_a:
                capital_b_game -= 1
                capital_a_game += 1
            else:
                capital_a_game -= 1
                capital_b_game += 1
        durations.append(time)
    return durations


plt.hist(player_ruin_duration(1000, 0.2, 50, 50), bins=100, density=True)
plt.show()
plt.hist(player_ruin_duration(1000, 0.5, 50, 50), bins=100, density=True)
plt.show()
plt.hist(player_ruin_duration(1000, 0.8, 50, 50), bins=100, density=True)
plt.show()
print("Average duration of the game when probability of player a is winning = 1/5: ", end='')
print(np.mean(player_ruin_duration(100, 0.2, 50, 50)))
print("Average duration of the game when probability of player a is winning = 1/2: ", end='')
print(np.mean(player_ruin_duration(100, 0.5, 50, 50)))
print("Average duration of the game when probability of player a is winning = 4/5: ", end='')
print(np.mean(player_ruin_duration(100, 0.8, 50, 50)))
