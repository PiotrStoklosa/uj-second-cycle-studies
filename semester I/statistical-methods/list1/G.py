import random
import matplotlib.pyplot as plt


def player_ruin_trajectory(number_of_games, probability_of_winning_a, capital_a, capital_b):
    a_wins = 0
    trajectory_of_a_winning = []
    for i in range(0, number_of_games):
        capital_a_game = capital_a
        capital_b_game = capital_b
        while capital_a_game != 0 and capital_b_game != 0:
            win = random.random()
            if win < probability_of_winning_a:
                capital_b_game -= 1
                capital_a_game += 1
            else:
                capital_a_game -= 1
                capital_b_game += 1
        if capital_b_game == 0:
            a_wins += 1
        trajectory_of_a_winning.append(a_wins)
    return trajectory_of_a_winning


trajectory = player_ruin_trajectory(20, 0.5, 50, 50)
plt.bar([i for i in range(0, 20)], trajectory, width=1)
plt.xticks([i for i in range(0, 20)])
plt.show()
