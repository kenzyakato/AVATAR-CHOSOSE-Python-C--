import random
from random import shuffle
import numpy as np
import copy 
import os
import sys
global image_list
image_list=[]
path="C:/projects/AvatarRecommendationSystem/Assets/Resources"
labelpath="C:/projects/AvatarRecommendationSystem/Assets/Resources/label/"

allLabels={}

global ChoosenImage
ChoosenImage=[]
global ChoosenLabel
ChoosenLabel=[]
global dic
dic={}
global vectorindex
global labelvector
global imagevector
imagevector={}
vectorindex=[]
labelvector=[]
	
global times
times=0

#print("[INFO] loading images and labels...")

for i in range(1419):#获取所有标签和向量位数
	f = open (labelpath+str(i)+'.txt','r',encoding="utf-8")
	i = i + 1
	lines = f.readlines()
		

	for line in lines:
		line = line.split("：" )
		l = line[1].strip('\n').strip(' ')
		if l not in vectorindex :
			vectorindex.append(l)
			labelvector.append(0)
#print(i)
#print(label)
#print(vectorindex)
#print(labelvector)
i=0
for i in range(1419):#储存每一个图片和对应向量
	label=labelvector.copy()
	f = open (labelpath+str(i)+'.txt','r',encoding="utf-8")
		
	lines = f.readlines()
	for line in lines:
		line = line.split("：" )
		l = line[1].strip('\n').strip(' ')
		if l in vectorindex :
			j=vectorindex.index(l)
			label[j]=label[j]+1
	allLabels[i]=label
	#i = i + 1
	#print(i)
#print(allLabels)
def readLabels(imageN):
	labels=[]
	f = open (labelpath+str(imageN)+'.txt','r', encoding="utf-8")
	lines = f.readlines()
	for line in lines:
		#line=line.strip('\n')
		line = line.split("：" )
		l = line[1].strip('\n').strip(' ')
		labels.append(l)
	return labels
def getsimilar(vector):
	a = list(allLabels.values())
	mindist=[]
	for i in a :
		b=np.array(vector)
		dist = np.linalg.norm(i - b)

		mindist.append(dist)
	import heapq
	max_number = heapq.nlargest(20,mindist)
	min_number = heapq.nsmallest(100, mindist) 
	#print(min_number)
	min_index = []
	for t in min_number:
	    index = mindist.index(t)
	    min_index.append(index)
	    mindist[index] = 0
	for t in max_number:
	    index = mindist.index(t)
	    min_index.append(index)
	    mindist[index] = 0
	random.shuffle (min_index)
	#print(min_number)
	#print(min_index)
	return min_index
	

while len(image_list)<16:
	for i in range(16):
		imageN=random.randint(0,1419)
		if str(imageN)+".jpg" not in image_list:
			image_list.append(str(imageN)+".jpg")
imageN = sys.argv[1]
labelvector=np.load('a.npy')
labelvector=labelvector.tolist()
#print (labelvector)
for l in readLabels(imageN):#类别加权
	if l in vectorindex :
		i=vectorindex.index(l)
		labelvector[i]=labelvector[i]+1
a = np.array(labelvector)
np.save('a.npy',a)
Smimag=[]
Smimag=getsimilar(a)
ChoosenLabel=[]
image_list=[]
for imageN in ChoosenImage :
	
	if imageN not in image_list:
		image_list.append(str(imageN))
'''if times > 16 :
	for imageN in Smimag:
		if str(imageN+times)+".jpg" not in image_list:
			image_list.append(str(imageN+times)+".jpg")
else:'''
for imageN in Smimag:
	if str(imageN) not in image_list:
		image_list.append(str(imageN))
					
while len(image_list)<16:
	i=len(image_list)
	#print('image_list len'+str(i))
	#print('+++++++++++++++++++++++')
	if imageN not in image_list:
		image_list.append(Smimag[i])
while len(image_list)>16:
	i=len(image_list)
	image_list.pop()
	#print('-----------------------')

ChoosenImage=[]
#print(image_list)
shuffle(image_list)
print(image_list)