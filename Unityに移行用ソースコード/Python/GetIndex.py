import random
from random import shuffle
import numpy as np
import copy 
import os
global image_list
image_list=[]
path="C:/projects/AvatarRecommendationSystem/Assets/Resources"
labelpath="C:/projects/AvatarRecommendationSystem/Assets/Resources"+"/label/"


dic={}
allLabels={}
imagevector={}
def reset():

	global ChoosenImage
	ChoosenImage=[]
	global ChoosenLabel
	ChoosenLabel=[]
	global dic
	global vectorindex
	global labelvector
	global imagevector
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
			l = line[1].strip('/n').strip(' ')
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
			l = line[1].strip('/n').strip(' ')
			if l in vectorindex :
				j=vectorindex.index(l)
				label[j]=label[j]+1
		allLabels[i]=label
		#i = i + 1
		#print(i)
	print(allLabels)
	
	
	
	'''global image_list
	image_list=[]
	while len(image_list)<16:
		for i in range(16):
			imageN=random.randint(0,1419)
			#if str(imageN)+".jpg" not in image_list:
			#	image_list.append(str(imageN)+".jpg")
			if str(imageN) not in image_list:
				image_list.append(str(imageN))
	"""for i in image_list:
		print(i)"""
	print (image_list)
	#print (allLabels)'''
reset()
