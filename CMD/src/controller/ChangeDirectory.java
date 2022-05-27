package controller;

import java.io.File;

import utility.Constants;

public class ChangeDirectory {
	private String currentPath;
	
	public void moveToParentFolder(String command) {
		int count = (command.length() - command.replace(String.valueOf(".."), "").length())/2;   //".."이 문장에 몇개가 있는지 확인
		
		if(count != 1) {  //".."을 하나만 포함해야 함
			return;   
		}
		
		command = command.replace(" ", "");      //명령어의 빈칸을 모두 지움("cd     .."인 경우도 명령어로 인식하기 위함)
		if(command.equals("..") == !Constants.IS_CORRECT_COMMAND_ENTERED) {   //cd뒤에 ".."만 있는지 확인
			return;                              //입력값이 "cd.."인 경우에만 유효한 명령어로 인식
		}
		
		int endIndex = currentPath.lastIndexOf("\\");   //경로에서 현재 위치한 폴더를 지움(상위폴더까지만 표시)
		currentPath = currentPath.substring(0, endIndex);
		
		if(currentPath.indexOf("\\") == -1) currentPath += "\\";
	}
	public void moveToRootFolder() {
		int endIndex = currentPath.indexOf("\\") + 1;
		currentPath = currentPath.substring(0, endIndex);
	}
	public String getCommand(String command) {
		command = command.toLowerCase();
		
		if(command.contains("..")) {
			return "..";
		}
		if(command.contains("\\")) {
			return "\\";
		}
		return command;
	}
	public void executeCommand(String command) {
		
		switch(getCommand(command)) {
		case "..":     //상위폴더로 이동
			moveToParentFolder(command);
			break;
		case "\\":     //루트폴더로 이동
			moveToRootFolder();
			break;
		case "":       //cd만 입력 -> 현재경로 출력
			System.out.println(currentPath + "\n");
			break;
		}
	}
	public String execute(String pathBeforeChange, String command) {
		this.currentPath = pathBeforeChange;
		int beginIndex = command.indexOf("cd") + 2;
		
		command = command.substring(beginIndex);   //cd다음에 이어지는 명령어로 구별해서 기능을 수행함
		executeCommand(command);
		
		return this.currentPath;
	}
}
