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
import view.Start;

public class CmdManagement {
	private String path;
	
	private CommandUsage commandUsage;
	private ChangeDirectory changeDirectory;
	private Copy copy;
	private Move move;
	
	private Start start;
	private Help helpCommand;
	private ClearScreen clearScreen;
	
	public CmdManagement() {
		path = "C:\\Users\\" + System.getProperty("user.name");
		
		commandUsage = new CommandUsage();
		changeDirectory = new ChangeDirectory();
		copy = new Copy();
		move = new Move();
		
		start = new Start();
		helpCommand = new Help();
		clearScreen = new ClearScreen();
	}
	public String inputWord(){
		System.out.print(String.format("\n%s>", path));
		
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
	public boolean isStartWithCorrectCommand(String command, String commandEntered) {
		int lengthOfCommand = command.length();
		int lengthOfCommandEntered = commandEntered.length();
		
		if(lengthOfCommandEntered < lengthOfCommand) {
			return !Constants.IS_START_WIDTH_CORRECT_COMMAND;
		}
		if(commandEntered.substring(0, lengthOfCommand).equals(command) 
				== !Constants.IS_STRING_MATCHED) {
			return !Constants.IS_START_WIDTH_CORRECT_COMMAND;
		}
		
		return Constants.IS_START_WIDTH_CORRECT_COMMAND;
	}
	public String getCommand(String commandEntered) {
		String command = commandEntered.toLowerCase();
		
		if(isStartWithCorrectCommand("cmd", command)) {
			return "cmd";
		}
		if(isStartWithCorrectCommand("help", command)) {
			return "help";
		}
		if(isStartWithCorrectCommand("cls", command)) {
			return "cls";
		}
		if(isStartWithCorrectCommand("cd", command)) {
			return "cd";
		}
		if(isStartWithCorrectCommand("move", command)) {
			return "move";
		}
		if(isStartWithCorrectCommand("copy", command)) {
			return "copy";
		}
		return commandEntered;
	}
	public void executeCommand(String commandEntered) {
		commandEntered = commandEntered.trim();            //입력한 명령어 앞뒤 공백제거
		
		String command = getCommand(commandEntered);  
		int endIndex = commandEntered.indexOf(' ');        //'abc ddd'입력시 'abc'를 명령어로 인식
		
		if(endIndex == -1) {
			endIndex = commandEntered.length();
		}
		
		switch(command) {
		case "cmd":
			start.printWindowVersion();
			break;
		case "help":
			helpCommand.execute();
			break;
		case "cls":
			clearScreen.execut();
			break;
		case "cd":
			path = changeDirectory.execute(path, commandEntered);
			break;
		case "move":
			move.execute(path, commandEntered);
			break;
		case "copy":
			copy.execute(path, commandEntered);
			break;
		case "":
			break;
		default:
			System.out.println("'" + commandEntered.substring(0, endIndex) 
			+ "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\r\n" + "배치 파일이 아닙니다.");
		}
	}
	public void start(){
		String command;
	
		start.printWindowVersion();
		
		while(true) {
			command = inputWord();
			executeCommand(command);
		}
	}
}
