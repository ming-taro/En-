package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

import dto.FilePath;
import utility.Constants;

public class Copy {
	protected FilePath filePath;
	private FilePathSetting filePathSetting;
	
	public Copy() {
		filePathSetting = new FilePathSetting();
		filePath = new FilePath();
	}
	protected boolean isFileExisting(String filePath) {
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
	private void copyFile(String firstFilePath, String secondFilePath) {
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
	protected void manageFileCopy(String firstFilePath, String secondFilePath) {
		String whetherToCopy = "";
		String firstFile = filePath.getFirstFile();
		String secondFile = filePath.getSecondFile();

		if(firstFile.equals(secondFile)
			|| isFileExisting(secondFilePath) == !Constants.IS_FILE_EXISTING) {  
			copyFile(firstFilePath, secondFilePath); //같은 파일에 덮어쓰려는 경우 (ex: copy a.txt) or 새로운 파일을 생성해 복사할 경우
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
			copyFile(firstFilePath, secondFilePath);
		}
		else {
			System.out.println("        0개 파일이 복사되었습니다.");
		}
		
	}
	private void executeCommand() {
		String firstFilePath = filePathSetting.getPathOfFirstFile(filePath);     //입력받은 파일 경로 구하기
		String secondFilePath = filePathSetting.getPathOfSecondFile(filePath);
		
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
		filePath.init();
	}
	public void execute(String path, String command) {
		filePath.setCurrentPath(path);  //현재 경로
		
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		if(command.equals("copy")) {  //copy만 입력하는 경우
			System.out.println("명령 구문이 올바르지 않습니다.");
			return;                
		}
		
		filePathSetting.setFilePath(command, filePath);    //파일경로 구하기
		executeCommand();
	}
}
