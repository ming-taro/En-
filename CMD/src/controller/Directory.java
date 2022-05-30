package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Directory {
	private CmdConnector cmdConnector;
	
	public Directory() {
		cmdConnector = new CmdConnector();
	}
	public void printDirectory(String path) {
		System.out.println(cmdConnector.getCmdExecutionResult("vol c:"));  //디스크 일련번호

		File file = new File(path); 
        File[] fList = file.listFiles(); 
         
        for( int i = 0; i < fList.length; i++ ) { 
        	long lastModified = fList[i].lastModified();

    		String pattern = "yyyy-MM-dd aa hh:mm";
    		SimpleDateFormat simpleDateFormat = new SimpleDateFormat(pattern);
    		
    		Date lastModifiedDate = new Date( lastModified );

    		System.out.println(simpleDateFormat.format(lastModifiedDate) + " / " + fList[i].getName());
        } 
		
	}
	public void executeCommand(String command) {
		command = command.toLowerCase();
		
		switch(command) {
		case "dir":
			
		}
		
	}
	public void execute(String path, String command) {
		//int beginIndex = command.indexOf(' ') + 1;
		
		//command = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문
		printDirectory(path);
		//executeCommand(command);
	}
}
