package controller;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import view.CommandUsage;
import view.Help;
import view.Start;

public class CmdManagement {
	private CommandUsage commandUsage;
	private Help helpCommand;
	
	public CmdManagement() {
		commandUsage = new CommandUsage();
		helpCommand = new Help();
	}
	public void executeCommand(String command) {
		command = command.toLowerCase();
		
		switch(command) {
		case "help":
			helpCommand.execute();
		}
	}
	public void start(){
		Start start = new Start();
		String command;
		
		start.printWindowVersion();
		
		while(true) {
			command = inputWord();
			executeCommand(command);
		}
	}
	public String inputWord(){
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
}
