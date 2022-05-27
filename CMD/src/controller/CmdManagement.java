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
	
	private Start start;
	private Help helpCommand;
	private ClearScreen clearScreen;
	
	public CmdManagement() {
		path = "C:\\Users\\" + System.getProperty("user.name");
		
		commandUsage = new CommandUsage();
		changeDirectory = new ChangeDirectory();
		
		start = new Start();
		helpCommand = new Help();
		clearScreen = new ClearScreen();
	}
	public String inputWord(){
		System.out.print(path + ">");
		
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
		commandEntered = commandEntered.toLowerCase();
		
		if(isStartWithCorrectCommand("cmd", commandEntered)) {
			return "cmd";
		}
		if(isStartWithCorrectCommand("help", commandEntered)) {
			return "help";
		}
		if(isStartWithCorrectCommand("cls", commandEntered)) {
			return "cls";
		}
		if(isStartWithCorrectCommand("cd", commandEntered)) {
			System.out.println("오");
			return "cd";
		}
		if(isStartWithCorrectCommand("move", commandEntered)) {
			return "move";
		}
		if(isStartWithCorrectCommand("copy", commandEntered)) {
			return "copy";
		}
		return "";
	}
	public void executeCommand(String commandEntered) {
		commandEntered = commandEntered.trim();            //입력한 명령어 앞뒤 공백제거
		String command = getCommand(commandEntered);  
		
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
			break;
		case "copy":
			break;
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
