package controller;

import java.io.File;

import utility.Constants;

public class FilePathSetting {
	public boolean isValidPath(String filePath) {
		int endIndex = filePath.lastIndexOf("\\");
		File file;
		
		filePath = filePath.substring(0, endIndex);       //파일이 위치한 폴더의 경로
		file = new File(filePath);
		
		if (file.isDirectory()) {                         //폴더가 존재하는지 검사
			 return Constants.IS_VALID_PATH;
		}
		return !Constants.IS_VALID_PATH;
	}
	public String getPathOfSecondFile(String firstFile, String secondFile, String currentPath) {
		if(secondFile.indexOf("\\") == -1) {         //파일이름만 입력한 경우 파일의 경로는 현재경로가 됨
			return currentPath + "\\" + secondFile;  //ex: a.txt -> C:\Users\sec\a.txt
		}
		else if(secondFile.indexOf(".") != -1) {     //파일경로를 입력한 경우
			return secondFile;
		}
		
		if(firstFile.indexOf("\\") == -1) {       //첫번째 파일이 a.txt, 두번째파일에 폴더경로만 있는 경우
			return secondFile + "\\" + firstFile; //ex: C:\Users\sec -> C:\Users\sec\a.txt     
		}
		else {
			return secondFile + firstFile.substring(firstFile.lastIndexOf("\\"));
		}
		/*첫번째 파일이 파일경로로 주어지고, 두번째 파일에 폴더경로만 있는 경우
		  ex: 첫번째파일: C:\Users\sec\a.txt
		           두번째파일: C:\Users\sec\Onedrive\"바탕 화면" -> C:\Users\sec\Onedrive\"바탕 화면"\a.txt*/
	}	
	public String getPathOfFirstFile(String firstFile, String currentPath) {
		if(firstFile.indexOf("\\") == -1) {         //파일이름만 입력한 경우 파일의 경로는 현재경로가 됨
			return currentPath + "\\" + firstFile;  //ex: a.txt -> C:\Users\sec\a.txt
		}
		return firstFile;
	}
}
