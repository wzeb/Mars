'''
A Recurrent Neural Network (LSTM) implementation example using TensorFlow..
Next word prediction after n_input words learned from text file.
A story is automatically generated if the predicted word is fed back as input.
Project: https://github.com/roatienza/Deep-Learning-Experiments
'''

from __future__ import print_function
import numpy as np
import tensorflow as tf
from tensorflow.contrib import rnn
import collections
import time
import os
import datetime

####################################################################################
# configure
####################################################################################
training_file = 'data.txt'  # 'filtered_only_to_58_filtered_with_adj-simple.txt'
test_file = 'data.txt'
learning_rate = 0.0001  # learning rate
epoch = 10  #
display_step = 1000  # interval of display current accuracy
record_step = 1000  # interval of record current accuracy to local file
n_input = 3  # sliding window size
n_hidden = 256  # number of units in RNN cell
mode_dir = 'mode/'  # directory of saved model
version = 3  # the version you want to save as
train_size = 0
test_size = 0
out_dir = 'output/output' + str(version) + '/'

####################################################################################
# loading training data
####################################################################################
start_time = time.time()


def elapsed(sec):
    if sec < 60:
        return str(sec) + " sec"
    elif sec < (60 * 60):
        return str(sec / 60) + " min"
    else:
        return str(sec / (60 * 60)) + " hr"


####################################################################################
# loading training data and build dictionary
####################################################################################
def read_data(fname):
    with open(fname) as f:
        content = f.readlines()
    content = [x.strip() for x in content]
    content = [word for i in range(len(content)) for word in content[i].split(',')]
    content = np.array(content)
    print("content size: " + str(len(content)))
    return content

training_data = read_data(training_file)
train_size = round(training_data.size * 0.9)
test_size = training_data.size - train_size
training_data = training_data[0 : int(train_size)]
print("Loaded data, words count :", training_data.size)

def add_voc(word) :
    if word not in dictionary :
        id = len(dictionary)
        dictionary[word] = id
        reverse_dictionary[id] = word
        return True
    return False
def build_dataset(words) :
    for word in words :
        add_voc(word)

'''
def build_dataset(words):
    count = collections.Counter(words).most_common()
    dictionary = dict()
    for word, _ in count:
        dictionary[word] = len(dictionary)
    reverse_dictionary = dict(zip(dictionary.values(), dictionary.keys()))
    return dictionary, reverse_dictionary
'''
dictionary = dict()
reverse_dictionary = dict()
build_dataset(training_data)
vocab_size = len(dictionary)
print('built dictionary, vocab_size :', vocab_size)

vocab_size += 20

####################################################################################
# tf Graph input
####################################################################################
x = tf.placeholder("float", [None, n_input, 1], name='x')
y = tf.placeholder("float", [None, vocab_size], name='y')

####################################################################################
# RNN output node weights and biases
####################################################################################
weights = {
    'out': tf.Variable(tf.random_normal([n_hidden, vocab_size]), name='weights')
}
biases = {
    'out': tf.Variable(tf.random_normal([vocab_size]), name='biases')
}


####################################################################################
# RNN define
####################################################################################
def RNN(x, weights, biases):
    # reshape to [1, n_input]
    x = tf.reshape(x, [-1, n_input])
    # Generate a n_input-element sequence of inputs
    # (eg. [had] [a] [general] -> [20] [6] [33])
    x = tf.split(x, n_input, 1)
    # 2-layer LSTM, each layer has n_hidden units.
    # Average Accuracy= 95.20% at 50k iter
    rnn_cell = rnn.MultiRNNCell([rnn.BasicLSTMCell(n_hidden), rnn.BasicLSTMCell(n_hidden) \
                                 ])
    # 1-layer LSTM with n_hidden units but with lower accuracy.
    # Average Accuracy= 90.60% 50k iter
    # Uncomment line below to test.txt
    # but comment out the 2-layer rnn.MultiRNNCell above
    # rnn_cell = rnn.BasicLSTMCell(n_hidden)
    # generate prediction
    outputs, states = rnn.static_rnn(rnn_cell, x, dtype=tf.float32)
    # there are n_input outputs but
    # we only want the last output
    return tf.matmul(outputs[-1], weights['out']) + biases['out']


####################################################################################
# prepare parameters
####################################################################################
pred = RNN(x, weights, biases)
cost = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(logits=pred, labels=y))
optimizer = tf.train.RMSPropOptimizer(learning_rate=learning_rate).minimize(cost)
correct_pred = tf.equal(tf.argmax(pred, 1), tf.argmax(y, 1))
accuracy = tf.reduce_mean(tf.cast(correct_pred, tf.float32))
init = tf.global_variables_initializer()
ave_accuracy_arr = []
ave_accuracy_test = 0
####################################################################################
# Launch the graph
####################################################################################

with tf.Session() as session:
    session.run(init)

    offset = 0  # offset of train data

    iters_per_epoch = 0  # count iter number per epoch
    acc_per_epoch = 0
    training_epochs = 0  # record current epoch number

    display_step = 0
    display_cnt = 0
    display_id = 0

    while True:
        if (training_epochs >= epoch):
            print('continue training ? (Y/N)')
            inp = input()
            if inp != 'Y':
                break
            else:
                epoch += 1
        # Generate a minibatch. Add some randomness on selection process.
        # restart taining procedure
        if offset > train_size - n_input  - 1:
            print("epoch = " + str(training_epochs + 1) + ", ave_Accuracy= " + \
                  "{:.2f}%".format(100 * acc_per_epoch / iters_per_epoch))
            ave_accuracy_arr.append(100 * acc_per_epoch / iters_per_epoch)
            iters_per_epoch = 0
            acc_per_epoch = 0
            training_epochs += 1
            offset = 0

            display_step = 0
            display_cnt = 0
            display_id = 0
            continue
        symbols_in_keys = [[dictionary[str(training_data[i])]] for i in range(offset, offset + n_input)]
        symbols_in_keys = np.reshape(np.array(symbols_in_keys), [-1, n_input, 1])
        symbols_out_onehot = np.zeros([vocab_size], dtype=float)
        symbols_out_onehot[dictionary[str(training_data[offset + n_input])]] = 1.0
        symbols_out_onehot = np.reshape(symbols_out_onehot, [1, -1])

        acc = session.run(accuracy, feed_dict={x: symbols_in_keys, y: symbols_out_onehot})
        session.run(optimizer, feed_dict={x: symbols_in_keys, y: symbols_out_onehot})
        acc_per_epoch += acc
        offset += 1
        iters_per_epoch += 1

        display_cnt += acc
        display_step += 1
        if display_step == 3000 :
            display_id += 1
            print('current acc(%d) : %f' % (display_id, display_cnt/30))
            display_cnt = 0
            display_step = 0

    # end while
    print("Optimization Finished!")
    print("Elapsed time: ", elapsed(time.time() - start_time))

    ############################################### Test ########################################

    print('\ntesting ...')
    words = read_data(test_file)
    start_idx = train_size - n_input     # start index of sliding window
    correct = 0  # count
    step = 0  # step number
    display_cnt = 0
    display_step = 0
    while start_idx < len(words) - n_input:
        step += 1
        symbols_in_keys = [dictionary[str(words[i + start_idx])] for i in range(n_input)]
        keys = np.reshape(np.array(symbols_in_keys), [-1, n_input, 1])
        add = add_voc(str(words[start_idx + n_input]))
        symbols_out_onehot = np.zeros([vocab_size], dtype=float)
        symbols_out_onehot[dictionary[str(words[start_idx + n_input])]] = 1.0
        symbols_out_onehot = np.reshape(symbols_out_onehot, [1, -1])

        acc = session.run(accuracy, feed_dict={x: keys, y: symbols_out_onehot})
        session.run(optimizer, feed_dict={x: keys, y: symbols_out_onehot})
        correct += acc
        start_idx += 1

        display_step += 1
        display_cnt += acc
        if display_step == 500 :
            print('current acc(per 500) : ',display_cnt/5)
            display_cnt = 0
            display_step = 0

    ave_accuracy_test = 100.0 * correct / step
    print('tested. ave_accuracy_test = '+ \
                  "{:.2f}%".format(ave_accuracy_test))


'''
save information
'''
print('\nsaving configuration ...')
if not os.path.exists(out_dir) :
        os.makedirs(out_dir)
with open(out_dir + 'configure.txt', 'w') as f :
    f.write('training_file : ' + training_file + '\n')
    f.write('test_file : ' + test_file + '\n')
    f.write('learning_rate : ' + str(learning_rate) + '\n')
    f.write('epoch : ' + str(epoch) + '\n')
    f.write('n_input : ' + str(n_input) + '\n')
    f.write('n_hidden : ' + str(n_hidden) + '\n')
    f.write('version : ' + str(version) + '\n')
    f.write('ave_accuracy_test : ' + str(ave_accuracy_test) + '\n')
    f.write('time : ' + str(datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')))
print('\nsaving training_info ...')
with open(out_dir + 'training_info.txt', 'w') as f :
    for acc in ave_accuracy_arr :
        f.write(str(acc) + '\n')
print('over.')
   