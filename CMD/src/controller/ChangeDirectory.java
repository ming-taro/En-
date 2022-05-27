package controller;

import java.io.File;

public class ChangeDirectory {
	public String moveToParentFolder(String path) {
		int endIndex = path.lastIndexOf("\\");
		path = path.substring(0, endIndex);
		
		if(path.indexOf("\\") == -1) path += "\\";
		
		return path;
	}
	public String moveToRootFolder(String path) {
		int endIndex = path.indexOf("\\") + 1;
		return path.substring(0, endIndex);
	}
	public String execute(String path, String command) {
		int beginIndex = command.indexOf("cd") + 2;
		
		command = command.substring(beginIndex);   //cd다음에 이어지는 명령어로 구별해서 기능을 수행함
		
		switch(command) {
		case "..":
			path = moveToParentFolder(path);
			break;
		case "\\":
			path = moveToRootFolder(path);
			break;
		}
		return path;
	}
}
