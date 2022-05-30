package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.file.Files;
import java.nio.file.LinkOption;
import java.nio.file.Path;
import java.nio.file.StandardCopyOption;

import utility.Constants;

public class Copy {
	private String currentPath;
	private String firstFile;
	private String secondFile;
	private FilePathSetting filePathSetting;
	
	public Copy() {
		filePathSetting = new FilePathSetting();
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
	private int getIndexOfPointThatSeparatesFileName(String command) {
		int indexOfDot = command.indexOf('.');
		int index = command.indexOf(' ', indexOfDot);   //첫번재 파일의 '.'이후로 빈칸이 나오는 지점이 두 파일을 구분하는 지점
		
		if(index == -1) {  //파일을 하나만 입력한 경우 -> ex: copy a.txt -> index = 5
			return command.length();
		}
		
		return index;      //ex: copy a.txt b.txt -> index = 5
	}
	private int getIndexOfPointThatSeparatesFilePath(String command) {
		if(command.indexOf("C:") == -1                //ex: copy a.txt b.txt or copy a.txt
				|| command.indexOf("C:", 2) == -1){   //ex: copy C:\\users\\a.txt b.txt or copy C:\\users\\a.txt
			return getIndexOfPointThatSeparatesFileName(command);
		}
		if(command.indexOf("C:", 2) != -1) {          //ex: copy C:\\users\\a.txt C:\\users\\b.txt
			return command.indexOf("C:", 2) - 1;      //ex: copy a.txt c:\\users\\b.txt
		}
		return -1;
	}
	private void setFilePath(String command) {
		int index = 0;
		int beginIndex = -1, endIndex = -1;
		
		while(index < command.length()) {
			if(command.charAt(index) == '"') {                
				command = RemoveQuotationMark(command, index);     //자바의 files.copy()는 빈칸이 있더라도 큰따옴표가 없어야 올바르게 인식 -> copy를 실행하기 위해 큰따옴표를 지워줌
			}
			index++;
		}
		
		endIndex = getIndexOfPointThatSeparatesFilePath(command);  //첫 번째 파일의 경로정보가 끝나는 지점의 인덱스 값 
		beginIndex = endIndex + 1;         	                       //두 번째 파일의 경로정보가 시작되는 지점의 인덱스 값
	     
		if(endIndex == command.length() && command.indexOf("\\") == -1) {  //ex: copy a.txt
			firstFile = currentPath + "\\" + command;                      //-> 현재경로\a.txt
			secondFile = firstFile;
		}
		else if(endIndex == command.length()) {//ex: copy C:\Users\sec\OneDrive\"바탕 화면"\a.txt
			firstFile = command;               //-> 두번재 파일: 현재경로
			secondFile = currentPath;           
		}
		else {                                                      //두번째 파일을 입력한 경우
			firstFile = command.substring(0, endIndex).trim();
			secondFile = command.substring(beginIndex).trim();
		}
	}
	private boolean isFileExisting(String filePath) {
		File file = new File(filePath);
		
		if(file.exists()) {
			return Constants.IS_FILE_EXISTING;
		}
		return !Constants.IS_FILE_EXISTING;
	}
	private boolean isChoosenToCopy(String whetherToCopy) {
		if(whetherToCopy == null || whetherToCopy.equals("")) {
			return !Constants.IS_CHOOSEN_TO_COPY;
		}
		
		switch(whetherToCopy.toLowerCase().charAt(0)) {
		case 'y': case 'a': case 'n':
			return Constants.IS_CHOOSEN_TO_COPY;
		default:
			return !Constants.IS_CHOOSEN_TO_COPY;
		}
	}
	private String inputWord() {
		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
		String word = "";
		
		try {
			word = reader.readLine();
		} catch (IOException e) {
			e.printStackTrace();
			System.out.println(e);
		}
		
		return word;
	}
	private void copyfile(String firstFilePath, String secondFilePath) {
		 File fileToCopy = new File(firstFilePath);       
		 File fileToSave = new File(secondFilePath);   
		  
		 if(firstFilePath.equals(secondFilePath)) {  //같은 파일에 덮어쓰려는 경우
				System.out.println("같은 파일로 복사할 수 없습니다.\r\n" +  "        0개 파일이 복사되었습니다.");
				return;
		}
		 
		 try {
			Files.copy(fileToCopy.toPath(), fileToSave.toPath(), 
					StandardCopyOption.REPLACE_EXISTING);
		} catch (IOException e) {
			e.printStackTrace();
		} 
		 
		System.out.println("        1개 파일이 복사되었습니다.");
	}
	private void manageFileCopy(String firstFilePath, String secondFilePath) {
		String whetherToCopy = "";
		
		if(firstFile.equals(secondFile)) {  //같은 파일에 덮어쓰려는 경우 -> ex: copy a.txt
			copyfile(firstFilePath, secondFilePath);
			return;
		}
		
		if(isFileExisting(secondFilePath) 
				== !Constants.IS_FILE_EXISTING) {   //새로운 파일을 생성해 복사할 경우
			copyfile(firstFilePath, secondFilePath);
			return;
		}
		
		while(Constants.IS_ENTERING_VALUE) {        //기존 파일에 덮어쓰는 경우 -> 덮어쓸지 여부를 선택함
			if(secondFile.indexOf('\\') == -1) {    //두번째 파일입력시 파일명을 입력한 경우(ex: copy a.txt b.txt)
				System.out.print(secondFile + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");  //-> 'b.txt'를 덮어쓰시겠습니
			}
			else {                                  //두번째 파일입력시 파일경로를 입력한 경우(ex: copy a.txt C:\Users\sec)
				System.out.print(secondFilePath + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");  //->'C:\Users\sec\a.txt'를 덮어쓰시겠습니까?
			}
			whetherToCopy = inputWord();
			if(isChoosenToCopy(whetherToCopy)) {     
				break;
			}
		}
		
		if(whetherToCopy.charAt(0) != 'n') {        //yes or all을 선택한 경우 -> 파일복사
			copyfile(firstFilePath, secondFilePath);
		}
		else {
			System.out.println("        0개 파일이 복사되었습니다.");
		}
		
	}
	private void executeCommand() {
		String firstFilePath;     //입력받은 파일 경로 구하기
		String secondFilePath;

		firstFilePath = filePathSetting.getPathOfFirstFile(firstFile, currentPath);
		secondFilePath = filePathSetting.getPathOfSecondFile(firstFile, secondFile, currentPath);
		
		if(filePathSetting.isValidPath(firstFilePath) == !Constants.IS_VALID_PATH
			|| filePathSetting.isValidPath(secondFilePath) == !Constants.IS_VALID_PATH) {  
			System.out.println("지정된 경로를 찾을 수 없습니다.");   //폴더경로가 잘못된 경우
			return;
		}
	
		if(isFileExisting(firstFilePath) == !Constants.IS_FILE_EXISTING) {
			System.out.println("지정된 파일을 찾을 수 없습니다.");   //복사할 파일이 존재하지 않는 경우
			return;                                         //(두 번째 파일이 존재하지 않는 경우는 새로 생성해서 복사)
		}
		
		manageFileCopy(firstFilePath, secondFilePath);      //파일 복사

		firstFile = "";
		secondFile = "";
	}
	public void execute(String path, String command) {
		this.currentPath = path;  //현재 경로
		
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		if(command.equals("copy")) {  //copy만 입력하는 경우
			System.out.println("명령 구문이 올바르지 않습니다.");
			return;                
		}
		
		setFilePath(command);                              //파일경로 구하기
		executeCommand();
	}
}
