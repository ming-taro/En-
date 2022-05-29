package controller;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

public class Copy {
	private void copyfile(String pathOfFileToCopy, String pathOfFileToSave) {
		 File fileToCopy = new File(pathOfFileToCopy);       
		 File fileToSave = new File(pathOfFileToSave);   
		  
		 try {
			Files.copy(fileToCopy.toPath(), fileToSave.toPath(), 
					StandardCopyOption.REPLACE_EXISTING);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
	}
	public String RemoveQuotationMark(String commandToChange, int currentPosition) {
		StringBuilder command = new StringBuilder();
		
		command.append(commandToChange);
		command.deleteCharAt(currentPosition);  //첫 번째 큰따옴표 삭제
		
		for(int index = currentPosition; index < command.length(); index++) {
			if(command.charAt(index) == '"') {
				command.deleteCharAt(index);    //두 번째 큰따옴표 삭제
				break;
			}
		}
		
		return command.toString();
	}
	private void executeCommand(String command) {
		int index = 0;
		
		while(index < command.length()) {
			//cmd에서는 빈칸이 있는 경우 큰따옴표로 묶여있어야 인식을 함 -> 입력받을때는 빈칸이 있는 경우 큰따옴표로 묶도록 함
			//자바의 files.copy()는 빈칸이 있더라도 큰따옴표가 없어야 올바르게 인식 -> copy를 실행하기 위해 큰따옴표를 지워줌
			if(command.charAt(index) == '"') {                 
				command = RemoveQuotationMark(command, index); 
			}
			index++;
		}
		
		/*String[] file = command.split("  ");
		
		System.out.println("<" + file[0] + ">" + "<" + file[1] + ">");
		copyfile(file[0].trim(), file[1].trim());*/
	}
	public void execute(String path, String command) {
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		
		executeCommand(command);
	}
}
