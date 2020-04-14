#pip install --ignore-installed --upgrade tensorflow==1.5
import tensorflow as tf

config = tf.ConfigProto()
config.gpu_options.allow_growth = True
sess = tf.Session(config=config)
import numpy as np
import matplotlib.pyplot as plt

input_size=1
output_size=1
data_size=2000

x_data=np.random.rand(data_size,input_size)
print(type(x_data))
y_data=np.zeros((data_size,output_size))
k,T,x_state=0.5,200,0
t_conv=np.e**(-1/T)
for i in range(data_size):
    x_state= t_conv*x_state + k*(1-t_conv)*x_data[i]
    y_data[i] = x_state

batch_size=50
hide_size=5

x=tf.placeholder(tf.float32,shape=(None,input_size))
y=tf.placeholder(tf.float32,shape=(None,output_size))
w_hidden=tf.Variable(tf.random_normal([input_size,hide_size],stddev=1,seed=1))
b_hidden=tf.Variable(tf.zeros([1,hide_size],dtype=tf.float32))
w_output=tf.Variable(tf.random_normal([hide_size,output_size],stddev=1,seed=1))

h=tf.nn.tanh(tf.matmul(x,w_hidden)+b_hidden)
y_pred=tf.nn.sigmoid(tf.matmul(h,w_output))

learning_rate=2e-3
cross_entropy=tf.reduce_mean(tf.square(y-y_pred))
train_step=tf.train.AdamOptimizer(learning_rate).minimize(cross_entropy)
saver = tf.train.Saver()
with tf.Session() as sess:
    init_op=tf.global_variables_initializer()
    sess.run(init_op)
    STEPS = 10000  # 训练次数
    for i in range(STEPS):
        start = max((i * batch_size) % 1000,20)
        end = min(start + batch_size, 1000)  # 取前1000点训练
        sess.run(train_step,feed_dict={x:x_data[start:end],y:y_data[start:end]})
        # 显示误差
        if i % 100 == 0:
            total_cross_entropy = sess.run(cross_entropy,
                                           feed_dict={x: x_data[1000:1500], y: y_data[1000:1500]})  # 1000~1500测试
            print('训练%d次后，误差为%f' % (i, total_cross_entropy))
            if total_cross_entropy <= 1e-3:
                break
        else:
            print('未达到训练目标')
            #exit()
        # 保存结果

    file_path="./test"
    save_path = saver.save(sess, file_path)
    predict = sess.run(y_pred, feed_dict={x: x_data})
predict=predict.ravel()
orange=y_data.ravel()
#建立时间轴
t = np.arange(2000)
plt.plot(t,predict)
plt.plot(t,orange)
plt.show()