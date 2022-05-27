package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.UnknownHostException;

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
	public String getCommand(String command) {
		command = command.toLowerCase();
		
		if(command.contains("cmd")) {
			return "cmd";
		}
		if(command.contains("help")) {
			return "help";
		}
		if(command.contains("cls")) {
			return "cls";
		}
		if(command.contains("cd")) {
			return "cd";
		}
		if(command.contains("cd")) {
			return "move";
		}
		if(command.contains("cd")) {
			return "copy";
		}
		return "";
	}
	public void executeCommand(String commandEntered) {
		String command = getCommand(commandEntered);  
		commandEntered = commandEntered.trim();            //입력한 명령어 앞뒤 공백제거
		
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
