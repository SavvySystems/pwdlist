from array import *


# ***  initCharArray  ***
def initCharArray():
  #48-57 = 0..9
  #65-90? = A..Z
  charArray = array('c')
  for number in range(48, 58):
    charArray.append(chr(number))
  for letter in range(65, 91):
    charArray.append(chr(letter))
  return charArray


# ***  incrementIndexArray  ***
def incrementIndexArray(indexArray, pos, maxIndex):
  #TODO: handle "overflow"
  if (pos >= 0):
    newIndex = indexArray[pos] + 1
    if (newIndex > maxIndex):
      indexArray[pos] = 0
      indexArray = incrementIndexArray(indexArray, (pos-1), maxIndex)
    else:
      indexArray[pos] = newIndex  
  return indexArray
  

# ***  isIndexArrayAllZeros  ***
def isIndexArrayAllZeros(indexArray):
  for i in range(len(indexArray)):
    if (indexArray[i] != 0):
      return 0
      break
  return 1


# ***  MAIN  ***
def main():

  minLength = 8
  maxLength = 8
  charArray = initCharArray()

  f = open("pwdlist_output.txt", "w+")
 
  for pwdLength in range(minLength, (maxLength+1)):

    #init array of "registers"
    indexArray = array('i')
    for i in range(pwdLength):
      indexArray.append(0)

    indexArray = incrementIndexArray(indexArray, (len(indexArray)-1), (len(charArray)-1))

    while (0 == isIndexArrayAllZeros(indexArray)):
	   
	#build pwd string
	previousChar = ""
	pwd = ""
	for position in range(len(indexArray)):
		index = indexArray[position]
		char = charArray[index]

		if char == previousChar:
			break

		pwd = pwd + char
		previousChar = char

	if (len(pwd) == pwdLength):
		f.write("%s\r\n" % pwd)

	indexArray = incrementIndexArray(indexArray, (len(indexArray)-1), (len(charArray)-1))

  f.close()


if __name__ == "__main__":
  main()

