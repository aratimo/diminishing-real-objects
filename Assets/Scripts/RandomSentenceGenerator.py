import random

with open('phrases2.txt', 'r') as file:
    sentences = file.readlines()

selected_sentences = random.sample(sentences, 50)

with open('D.txt', 'w') as file:
    for sentence in selected_sentences:
        file.write(sentence)
