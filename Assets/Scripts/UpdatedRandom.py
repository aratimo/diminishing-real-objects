import random

with open('phrases2.txt', 'r') as file:
    sentences = file.readlines()

random.shuffle(sentences)

selected_sentences = sentences[:250]

num_sentences_per_file = 50

files = ['0.txt', 'A.txt', 'B.txt', 'C.txt', 'D.txt']

for i, file_name in enumerate(files):
    start_index = i * num_sentences_per_file
    end_index = (i + 1) * num_sentences_per_file
    with open(file_name, 'w') as file:
        for sentence in selected_sentences[start_index:end_index]:
            file.write(sentence)