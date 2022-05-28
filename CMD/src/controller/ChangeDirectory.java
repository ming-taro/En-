package controller;

import java.io.File;

import utility.Constants;

public class ChangeDirectory {
	private String currentPath;
	
	public int getNumberOfWord(String sentence, String word) {
		int lengthOfSentence = sentence.length();
		int lengthOfWord = sentence.replace(String.valueOf(word), "").length();
		
		return (lengthOfSentence - lengthOfWord)/word.length();
	}
	public void moveToParentFolder(String command) {
		int endIndex;
		
		if(command.equals("..")) {                      //"cd.." -> 상위 폴더로 이동
			endIndex = currentPath.lastIndexOf("\\");   //경로에서 현재 위치한 폴더를 지움(상위폴더까지만 표시)
			currentPath = currentPath.substring(0, endIndex);
		}
		
		if(currentPath.indexOf("\\") == -1) currentPath += "\\";
	}
	public boolean isSlashCommand(String command) {
		if(command.equals("")) {   
			return true;           //(ex: cd\ or cd/)
		}
		if(getNumberOfWord(command, ".") == command.length()) {
			return true;           //(ex: cd\........ or cd/..........)
		}
		return false;
	}
	public void moveToRootFolder(String commandEntered) {
		int index = currentPath.indexOf("\\") + 1;
		String command;
		
		command = commandEntered.substring(1);   //cd/(or \)명령어 뒤에 이어지는 문자열에 따라 유효한 명령어인지 검사
		
		if(isSlashCommand(command)) {
			currentPath = currentPath.substring(0, index);
			return;
		}
		if(getNumberOfWord(command, "\\") > 0) {              //(ex: cd \\\\))
			System.out.println(String.format(
					"'%s'\nCMD에서 현재 디렉터리로 UNC 경로를 지원하지 않습니다.", commandEntered));
			return;
		}
		
		System.out.println("지정된 경로를 찾을 수 없습니다.");
	}
	public String getCommand(String command) {
		command = command.toLowerCase();
		
		if(command.contains("../")) {
			return "../";
		}
		if(command.charAt(0) == ('.')) {
			return "..";
		}
		if(command.charAt(0) == '\\') {
			return "\\";
		}
		if(command.charAt(0) == '/') {
			return "/";
		}
		return command;
	}
	public void executeCommand(String command) {
		
		switch(getCommand(command)) {
		case "../":              //상위폴더로 이동(ex cd ../)
			break;
		case "..":               //상위폴더로 이동(한 단계)(ex cd..)
			moveToParentFolder(command);
			break;
		case "\\": case "/":     //루트폴더로 이동(ex cd\ or cd/)
			moveToRootFolder(command);
			break;
		case "":                 //cd만 입력 -> 현재경로 출력
			System.out.println(currentPath + "\n");
			break;
		}
	}
	public String execute(String pathBeforeChange, String command) {
		this.currentPath = pathBeforeChange;
		int beginIndex = command.indexOf("cd") + 2;
		
		command = command.substring(beginIndex).trim();   //cd다음에 이어지는 명령어로 구별해서 기능을 수행함
		executeCommand(command);
		
		return this.currentPath;
	}
}
