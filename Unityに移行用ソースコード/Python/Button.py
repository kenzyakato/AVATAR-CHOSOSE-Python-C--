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

	
	for i in range(1419):#すべてのラベルとベクタービットを取得
		f = open (labelpath+str(i)+'.txt','r',encoding="utf-8")
		i = i + 1
		lines = f.readlines()
		

		for line in lines:
			line = line.split("：" )
			l = line[1].strip('\n').strip(' ')
			if l not in vectorindex :
				vectorindex.append(l)
				labelvector.append(0)
	a = np.array(labelvector)
	np.save('a.npy',a)
	global image_list
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
	#print (allLabels)

reset()
