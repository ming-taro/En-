package controller;

import java.io.File;

import utility.Constants;

public class ChangeDirectory {
	private String currentPath;
	
	private int getNumberOfWord(String sentence, String word) {
		int lengthOfSentence = sentence.length();
		int lengthOfWord = sentence.replace(String.valueOf(word), "").length();
		
		return (lengthOfSentence - lengthOfWord)/word.length();
	}
	private void moveToParentFolder(String command) {
		int endIndex = currentPath.lastIndexOf("\\");
		int level = getNumberOfWord(command, "../");
		
		for(int i = 0; i < level; i++) {
			currentPath = currentPath.substring(0, endIndex);
			if(currentPath.indexOf("\\") == -1) {
				currentPath += '\\';
				break;
			}
		}
	}
	private void moveOneLevelToParentFolder(String command) {
		int endIndex = currentPath.lastIndexOf("\\");
		
		if(command.equals("..")) {           //"cd.." -> 상위 폴더로 이동
			currentPath = currentPath.substring(0, endIndex);         //경로에서 현재 위치한 폴더를 지움(상위폴더까지만 표시)
			if(currentPath.indexOf("\\") == -1) currentPath += '\\';  //현재 경로가 루트경로일 경우 지워진 '\'표시를 다시 붙임
			return;
		}
		if(getNumberOfWord(command, ".") + getNumberOfWord(command, " ")
				== command.length()) {       //(ex: cd....... or cd. . . . . . . .)
			return;
		}
		
		System.out.println("지정된 경로를 찾을 수 없습니다.");
	}
	private boolean isSlashCommand(String command) {
		if(command.equals("")) {   
			return true;           //(ex: cd\ or cd/)
		}
		if(getNumberOfWord(command, ".") == command.length()) {
			return true;           //(ex: cd\........ or cd/..........)
		}
		return false;
	}
	private void moveToRootFolder(String commandEntered) {
		int endIndex = currentPath.indexOf("\\") + 1;
		String command;
		
		command = commandEntered.substring(1);   //cd/(or \)명령어 뒤에 이어지는 문자열에 따라 유효한 명령어인지 검사
		
		if(isSlashCommand(command)) {
			currentPath = currentPath.substring(0, endIndex);
			return;
		}
		if(getNumberOfWord(command, "\\") > 0) {              //(ex: cd \\\\))
			System.out.println(String.format(
			"'%s'\nCMD에서 현재 디렉터리로 UNC 경로를 지원하지 않습니다.", commandEntered));
			return;
		}
		
		System.out.println("지정된 경로를 찾을 수 없습니다.");
	}
	private void moveToPathEntered(String pathEntered) {
		File file;
		
		if(pathEntered.indexOf("\\") == -1) {   //'users'만 입력한 경우 -> 현재경로 + 입력값(디렉터리)
			if(currentPath.equals("C:\\")) {
				pathEntered = currentPath + pathEntered;  //현재 루트경로라면 '\\'표시를 앞에 넣지 않음
			}
			else pathEntered = currentPath + '\\' + pathEntered;
		}
		if(pathEntered.indexOf("C:") == -1) {   //'\\users'만 입력한 경우 -> C:\\users
			pathEntered = "C:" + pathEntered; 
		}
		
		file = new File(pathEntered);
		
		if(file.isDirectory()) {                //존재하는 디렉토리 경로인지 확인
			currentPath = pathEntered;
		}
		else if(file.isFile()) {                //입력한 경로가 파일일 경우
			System.out.println("디렉터리 이름이 올바르지 않습니다.");
		}
		else {
			System.out.println("지정된 경로를 찾을 수 없습니다.");
		}
	}
	private String getCommand(String command) {
		command = command.toLowerCase();
		
		if(command.length() == 0) {
			return command;
		}
		if(command.contains("../")) {
			return "../";
		}
		if(command.charAt(0) == ('.')) {
			return "..";
		}
		if(command.charAt(0) == '\\' 
			&& getNumberOfWord(command, "\\") == command.length()) {
			return "\\";
		}
		if(command.charAt(0) == '/') {
			return "/";
		}
		return command;
	}
	private void executeCommand(String command) {
		if(command.length() > 0
			&& command.charAt(command.length() - 1) == '?') {
			System.out.println("파일 이름, 디렉터리 이름 또는 볼륨 레이블 구문이 잘못되었습니다.");
			return;
		}
		command = command.replace("c:", "C:");
		
		switch(getCommand(command)) {
		case "../":              //상위폴더로 이동(ex cd ../)
			moveToParentFolder(command);
			break;
		case "..":               //상위폴더로 이동(한 단계)(ex cd..)
			moveOneLevelToParentFolder(command);
			break;
		case "\\": case "/":     //루트폴더로 이동(ex cd\ or cd/)
			moveToRootFolder(command);
			break;
		case "":                 //cd만 입력 -> 현재경로 출력
			System.out.println(currentPath);
			break;
		default:                 //cd뒤에 이동할 경로 입력(ex cd C:\\users)
			moveToPathEntered(command);
		}
	}
	public String execute(String pathBeforeChange, String command) {
		this.currentPath = pathBeforeChange;
		int beginIndex = command.indexOf("cd") + 2;
		
		command = command.substring(beginIndex).trim();   //cd다음에 이어지는 명령어로 구별해서 기능을 수행함
		String dirPath = currentPath + command;
		
		executeCommand(command);
		
		return this.currentPath;
	}
}
