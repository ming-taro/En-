package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.UnknownHostException;

import utility.Constants;
import view.ClearScreen;
import view.CommandUsage;
import view.Help;

public class CmdManagement {
	private String currentPath;
	
	private CommandUsage commandUsage;
	private ChangeDirectory changeDirectory;
	private Copy copy;
	private Move move;
	private Directory directory;
	
	private CmdConnector cmdConnector;
	private Help helpCommand;
	private ClearScreen clearScreen;
	
	public CmdManagement() {
		currentPath = "C:\\Users\\" + System.getProperty("user.name");
		
		commandUsage = new CommandUsage();
		changeDirectory = new ChangeDirectory();
		copy = new Copy();
		move = new Move();
		directory = new Directory();
		
		cmdConnector = new CmdConnector();
		helpCommand = new Help();
		clearScreen = new ClearScreen();
	}
	public String inputWord(){
		System.out.print(String.format("\n%s>", currentPath));
		
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
	public boolean isCdCommand(String commandEntered) {
		if(commandEntered.equals("cd")) {
			return Constants.IS_CD_COMMAND;
		}
		if(commandEntered.indexOf("cd") != 0) {
			return !Constants.IS_CD_COMMAND;
		}
		
		int beginIndex = commandEntered.indexOf("cd") + "cd".length();
		commandEntered = commandEntered.substring(beginIndex);       //cd뒤에 이어지는 명령어

		switch(commandEntered.charAt(0)){
		case '.': case '/': case '\\': case ' ':       //ex: cd.. or cd\
			return Constants.IS_CD_COMMAND;
		}
		return !Constants.IS_CD_COMMAND;               //ex: cdffff
	}
	public boolean isCorrectCommandEntered(String command, String commandEntered) {
		if(commandEntered.equals(command)) {
			return Constants.IS_CORRECT_COMMAND_ENTERED;
		}
		if(commandEntered.indexOf(command) != 0) {            //명령어로 시작하지 않음
			return !Constants.IS_CORRECT_COMMAND_ENTERED;
		}
		
		int beginIndex = command.indexOf(command) + command.length();
		commandEntered = commandEntered.substring(beginIndex);       //명령어뒤에 이어지는 문장
		
		if(commandEntered.charAt(0) == ' '){     //명령어 뒤에 실행할 문장은 명령어와 최소 한칸 이상 구별해서 작성해야 함 
			return Constants.IS_CORRECT_COMMAND_ENTERED;
		}
		return !Constants.IS_CORRECT_COMMAND_ENTERED;
	}
	public String getCommand(String commandEntered) {
		String command = commandEntered.toLowerCase();
		
		if(command.equals("cmd")) {
			return "cmd";
		}
		if(command.equals("help")) {
			return "help";
		}
		if(command.equals("cls")) {
			return "cls";
		}
		if(isCdCommand(command)) {
			return "cd";
		}
		if(isCorrectCommandEntered("move", command)) {
			return "move";
		}
		if(isCorrectCommandEntered("copy", command)) {
			return "copy";
		}
		if(isCorrectCommandEntered("dir", command)) {
			return "dir";
		}
		return commandEntered;
	}
	public void executeCommand(String commandEntered) {
		commandEntered = commandEntered.trim();            //입력한 명령어 앞뒤 공백제거
		
		String command = getCommand(commandEntered);  
		int endIndex = commandEntered.indexOf(' ');        //'abc ddd'입력시 공백 전까지의 문자인 'abc'를 명령어로 인식
		
		if(endIndex == -1) {
			endIndex = commandEntered.length();            //공백이 없는 문자 입력시
		}
		
		switch(command) {
		case "cmd":
			printWindowVersion();
			break;
		case "help":
			helpCommand.execute();
			break;
		case "cls":
			clearScreen.execut();
			break;
		case "cd":
			currentPath = changeDirectory.execute(currentPath, commandEntered);
			break;
		case "move":
			move.execute(currentPath, commandEntered);
			break;
		case "copy":
			copy.execute(currentPath, commandEntered);
			break;
		case "dir":
			directory.execute(currentPath, commandEntered);
			break;
		case "":
			break;
		default:
			System.out.println("'" + commandEntered.substring(0, endIndex) 
			+ "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\r\n" + "배치 파일이 아닙니다.");
		}
	}
	private void printWindowVersion() {
		System.out.print(cmdConnector.getCmdExecutionResult("ver").substring(1));  //처음 입력된 줄바꿈을 지움 
		System.out.println("(c) Microsoft Corporation. All rights reserved.");
	}
	public void start(){
		String command;
	
		printWindowVersion();
		
		while(true) {
			command = inputWord();
			executeCommand(command);
		}
	}
}
