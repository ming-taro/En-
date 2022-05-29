package controller;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

import utility.Constants;

public class Copy {
	private String firstFilePath;
	private String secondFilePath;
	
	public String RemoveQuotationMark(String commandToChange, int currentPosition) {
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
	private void executeCommand(String command) {
		int index = 0;
		int beginIndex = -1, endIndex = -1;
		
		while(index < command.length()) {
			if(command.charAt(index) == '"') {                  //cmd에서는 빈칸이 있는 경우 큰따옴표로 묶여있어야 인식을 함 -> 입력받을때는 빈칸이 있는 경우 큰따옴표로 묶도록 함
				command = RemoveQuotationMark(command, index);  //자바의 files.copy()는 빈칸이 있더라도 큰따옴표가 없어야 올바르게 인식 -> copy를 실행하기 위해 큰따옴표를 지워줌
			}
			index++;
		}
		if(command.indexOf('"') != -1) {                        //지워지지 않은 따옴표가 남아있는 경우 -> 유효하지 않은 파일 경로
			System.out.println("지정된 파일을 찾을 수 없습니다.");
			return;
		}
		
		endIndex = getIndexOfPointThatSeparatesFilePath(command);  //첫 번째 파일의 경로정보가 끝나는 지점의 인덱스 값 
		beginIndex = endIndex + 1;                                 //두 번째 파일의 경로정보가 시작되는 지점의 인덱스 값
		
		firstFilePath = command.substring(0, endIndex).trim();
		secondFilePath = command.substring(beginIndex).trim();
	}
	private boolean isFileExisting(String filePath) {
		File file = new File(filePath);
		
		if(file.exists()) {
			System.out.println("I find the existFile.txt");
			return Constants.IS_FILE_EXISTING;
		}
		System.out.println("No, there is not a no file.");
		return !Constants.IS_FILE_EXISTING;
	}
	private boolean isValidPath() {
		if(isFileExisting(firstFilePath) == !Constants.IS_FILE_EXISTING) {
			return !Constants.IS_VALID_PATH;
		}
		if(isFileExisting(secondFilePath) == !Constants.IS_FILE_EXISTING) {
			return !Constants.IS_VALID_PATH;
		}
		return Constants.IS_VALID_PATH;
	}
	private void copyfile() {
		 File fileToCopy = new File(firstFilePath);       
		 File fileToSave = new File(secondFilePath);   
		  
		 try {
			Files.copy(fileToCopy.toPath(), fileToSave.toPath(), 
					StandardCopyOption.REPLACE_EXISTING);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
	}
	public void execute(String path, String command) {
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		
		executeCommand(command);                           //파일경로 구하기
		
		if(firstFilePath == null) return;                  //유효하지 않은 파일 경로
		
		if(isValidPath() == !Constants.IS_VALID_PATH) {    //유효하지 않은 파일 경로
			System.out.println("지정된 파일을 찾을 수 없습니다.");
			return;
		}
		System.out.println(firstFilePath);
		System.out.println(secondFilePath);
		copyfile();
	}
}
