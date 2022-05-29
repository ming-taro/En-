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
	
	public String RemoveQuotationMark(String commandToChange, int currentPosition) { //큰따옴표 삭제
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
				&& (command.charAt(index + 1) == '\\' 
					|| command.charAt(index + 1) == '.')) {
				command.deleteCharAt(index);    //두 번째 큰따옴표 삭제
				return command.toString();
			}
		}
		System.out.println("<>" + commandToChange);
		return commandToChange;
	}
	private int getIndexOfPointThatSeparatesFilePath(String command) {
		if(command.indexOf("C:") == -1){              //ex: copy a.txt b.txt
			return command.indexOf(" ");
		}
		if(command.indexOf("C:", 2) != -1) {          //ex: copy c:\\users\\a.txt c:\\users\\b.txt
			return command.indexOf("C:", 2) - 1;      //ex: copy a.txt c:\\users\\b.txt
		}
		if(command.indexOf("C:", 2) == -1) {          //ex: copy c:\\users\\a.txt b.txt
			return command.lastIndexOf(" ");
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
		
		if(endIndex == -1) return;
		
		firstFile = command.substring(0, endIndex).trim();
		secondFile = command.substring(beginIndex).trim();
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
		case 'y': case 'a':
			System.out.println("        1개 파일이 복사되었습니다.");
			break;
		case 'n':
			System.out.println("        0개 파일이 복사되었습니다.");
			break;
		default:
			return !Constants.IS_CHOOSEN_TO_COPY;
		}
		return Constants.IS_CHOOSEN_TO_COPY;
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
		  
		 try {
			Files.copy(fileToCopy.toPath(), fileToSave.toPath(), 
					StandardCopyOption.REPLACE_EXISTING);
		} catch (IOException e) {
			e.printStackTrace();
		} 
	}
	private void manageFileCopy(String firstFilePath, String secondFilePath) {
		String whetherToCopy = "";
		
		if(isFileExisting(secondFilePath) 
				== !Constants.IS_FILE_EXISTING) {   //새로운 파일을 생성해 복사할 경우
			System.out.println("        1개 파일이 복사되었습니다.");
			copyfile(firstFilePath, secondFilePath);
			return;
		}
		
		while(Constants.IS_ENTERING_VALUE) {        //기존 파일에 덮어쓴느 경우 -> 덮어쓸지 여부를 선택함
			System.out.print(secondFile + "을(를) 덮어쓰시겠습니까? (Yes/No/All): ");
			whetherToCopy = inputWord();
			if(isChoosenToCopy(whetherToCopy)) {    
				break;
			}
		}
		
		if(whetherToCopy.charAt(0) != 'n') {        //yes or all을 선택한 경우 -> 파일복사
			copyfile(firstFilePath, secondFilePath);
		}
		
	}
	private String getPathOfFile(String filePath) {
		if(filePath.indexOf("\\") == -1) {         //파일이름만 입력한 경우 파일의 경로는 현재경로가 됨
			return currentPath + "\\" + filePath;  //ex: a.txt -> C:\Users\sec\a.txt
		}
		return filePath;
	}
	private boolean isValidPath(String filePath) {
		int endIndex = filePath.lastIndexOf("\\");
		File file;
		
		filePath = filePath.substring(0, endIndex);       //파일이 위치한 폴더의 경로
		file = new File(filePath);
		
		if (file.isDirectory()) {                         //폴더가 존재하는지 검사
			 return Constants.IS_VALID_PATH;
		}
		return !Constants.IS_VALID_PATH;
	}
	private void executeCommand() {
		String firstFilePath = getPathOfFile(firstFile);     //입력받은 파일 경로 구하기
		String secondFilePath = getPathOfFile(secondFile);
		
		if(isValidPath(firstFilePath) == !Constants.IS_VALID_PATH
			|| isValidPath(secondFilePath) == !Constants.IS_VALID_PATH) {  
			System.out.println("지정된 경로를 찾을 수 없습니다.");   //폴더경로가 잘못된 경우
			return;
		}
	
		if(isFileExisting(firstFilePath) == !Constants.IS_FILE_EXISTING) {
			System.out.println("지정된 파일을 찾을 수 없습니다.");   //복사할 파일이 존재하지 않는 경우
			return;                                         //(두 번째 파일이 존재하지 않는 경우는 새로 생성해서 복사)
		}
		
		manageFileCopy(firstFilePath, secondFilePath);      //파일 복사
	}
	public void execute(String path, String command) {
		this.currentPath = path;  //현재 경로
		
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		
		setFilePath(command);                              //파일경로 구하기
		
		if(firstFile == null) {  //copy만 입력하는 경우
			System.out.println("명령 구문이 올바르지 않습니다.");
			return;                
		}

		System.out.println(firstFile);
		System.out.println(secondFile);
		 
		executeCommand();
	}
}
