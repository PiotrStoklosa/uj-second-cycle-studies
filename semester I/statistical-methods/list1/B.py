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


probability_of_a_ruin = []
probability_of_a_ruin_analytical = []
probability = [0.5 + i / 1000 for i in range(-100, 101)]

for i in range(-100, 101):
    probability_of_a_winning_a_single_game = 0.5 + i / 1000
    q = probability_of_a_winning_a_single_game
    p = 1 - q
    n = 50
    z = 100
    if p == q:
        probability_of_a_ruin_analytical.append(1 - n/z)
    else:
        pq = p/q
        r_n = (pq ** n - pq ** z) / (1 - pq ** z)
        probability_of_a_ruin_analytical.append(r_n)
    probability_of_a_ruin.append(player_ruin(100, probability_of_a_winning_a_single_game, 50, 50) / 100)
plt.plot(probability, probability_of_a_ruin)
plt.plot(probability, probability_of_a_ruin_analytical)
plt.show()

