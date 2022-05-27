package controller;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import view.CommandUsage;
import view.Start;

public class CmdManagement {
	private CommandUsage commandUsage;
	
	public CmdManagement() {
		commandUsage = new CommandUsage();
	}
	public void start(){
		Start start = new Start();
		start.printWindowVersion();
		commandUsage.printDirUsage();
		
		//inputWord();
	}
	public void inputWord(){
		BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
		String str = "";
		
		try {
			str = reader.readLine();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		System.out.println(str);
	}
}
