import random
import matplotlib.pyplot as plt


def player_ruin(number_of_games, probability_of_winning_a, capital_a, capital_b):
    a_lost = 0
    for i in range(0, number_of_games):
        capital_a_game = capital_a
        capital_b_game = capital_b
        while capital_a_game != 0 and capital_b_game != 0:
            if random.random() < probability_of_winning_a:
                capital_b_game -= 1
                capital_a_game += 1
            else:
                capital_a_game -= 1
                capital_b_game += 1
        if capital_a_game == 0:
            a_lost += 1
    return a_lost


numerical_test = []

# numerical solution

for i in range(1, 100):
    numerical_test.append(player_ruin(100, 0.5, i, 100 - i))
plt.plot(numerical_test, 'ro')
plt.show()

# analytical solution
# for i in range(1, 100):
#     numerical_test.append(player_ruin(100, 0.5, i, 100 - i))
plt.plot([1 - i / 100 for i in range(1,100)], 'ro')
plt.show()
