package controller;

import java.io.File;

import model.FilePath;
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
	public String getPathOfSecondFile(FilePath filePath) {
		String firstFile = filePath.getFirstFile();
		String secondFile = filePath.getSecondFile();
		String currentPath = filePath.getCurrentPath();
		
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
	public String getPathOfFirstFile(FilePath filePath) {
		String firstFile = filePath.getFirstFile();
		String currentPath = filePath.getCurrentPath();
		
		if(firstFile.indexOf("\\") == -1) {         //파일이름만 입력한 경우 파일의 경로는 현재경로가 됨
			return currentPath + "\\" + firstFile;  //ex: a.txt -> C:\Users\sec\a.txt
		}
		return firstFile;
	}
	public int getIndexOfPointThatSeparatesFileName(String command) {
		int indexOfDot = command.indexOf('.');
		int index = command.indexOf(' ', indexOfDot);   //첫번재 파일의 '.'이후로 빈칸이 나오는 지점이 두 파일을 구분하는 지점
		
		if(index == -1) {  //파일을 하나만 입력한 경우 -> ex: copy a.txt -> index = 5
			return command.length();
		}
		
		return index;      //ex: copy a.txt b.txt -> index = 5
	}
	public int getIndexOfPointThatSeparatesFilePath(String command) {
		if(command.indexOf("C:") == -1                //ex: copy a.txt b.txt or copy a.txt
				|| command.indexOf("C:", 2) == -1){   //ex: copy C:\\users\\a.txt b.txt or copy C:\\users\\a.txt
			return getIndexOfPointThatSeparatesFileName(command);
		}
		if(command.indexOf("C:", 2) != -1) {          //ex: copy C:\\users\\a.txt C:\\users\\b.txt
			return command.indexOf("C:", 2) - 1;      //ex: copy a.txt c:\\users\\b.txt
		}
		return -1;
	}
	private String RemoveQuotationMark(String commandToChange, int currentPosition) { //큰따옴표 삭제
		StringBuilder command = new StringBuilder();
		
		command.append(commandToChange);
		
		if(command.charAt(currentPosition - 1) == '\\') {
			command.deleteCharAt(currentPosition);  //첫 번째 큰따옴표 삭제
		}
		else {
			return commandToChange;
		}
		
		for(int index = currentPosition; index < command.length(); index++) {
			if(command.charAt(index) == '"' 
				&& ((index + 1) < command.length()
				&& command.charAt(index + 1) == '\\')
				|| (index + 1) == command.length()) {
				command.deleteCharAt(index);       //두 번째 큰따옴표 삭제
				return command.toString();
			}
		}
		return commandToChange;
	}
	public void setFilePath(String command, FilePath filePath) {
		int index = 0;
		int beginIndex = -1, endIndex = -1;
		String currentPath = filePath.getCurrentPath();
		
		while(index < command.length()) {
			if(command.charAt(index) == '"') {                
				command = RemoveQuotationMark(command, index);     //자바의 files.copy()는 빈칸이 있더라도 큰따옴표가 없어야 올바르게 인식 -> copy를 실행하기 위해 큰따옴표를 지워줌
			}
			index++;
		}
		
		endIndex = getIndexOfPointThatSeparatesFilePath(command);  //첫 번째 파일의 경로정보가 끝나는 지점의 인덱스 값 
		beginIndex = endIndex + 1;         	                       //두 번째 파일의 경로정보가 시작되는 지점의 인덱스 값
	     
		if(endIndex == command.length() && command.indexOf("\\") == -1) {  //ex: copy a.txt
			filePath.setFirstFile(currentPath + "\\" + command);                      //-> 현재경로\a.txt
			filePath.setSecondFile(filePath.getFirstFile());
		}
		else if(endIndex == command.length()) {//ex: copy C:\Users\sec\OneDrive\"바탕 화면"\a.txt
			filePath.setFirstFile(command);               //-> 두번재 파일: 현재경로
			filePath.setSecondFile(currentPath);           
		}
		else {                                                      //두번째 파일을 입력한 경우
			filePath.setFirstFile(command.substring(0, endIndex).trim());
			filePath.setSecondFile(command.substring(beginIndex).trim());
		}
	}
}
