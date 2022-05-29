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
			Files.copy(fileToCopy.toPath(), fileToSave.toPath(), StandardCopyOption.REPLACE_EXISTING);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
	}
	private void executeCommand(String command) {
		String[] file = command.split("  ");
		
		System.out.println("<" + file[0] + ">" + "<" + file[1] + ">");
		copyfile(file[0].trim(), file[1].trim());
	}
	public void execute(String path, String command) {
		int beginIndex = command.indexOf(' ') + 1;
		
		command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		
		executeCommand(command);
	}
}
