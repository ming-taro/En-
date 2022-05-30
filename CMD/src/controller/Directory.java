package controller;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;

import model.DirectoryInformation;

public class Directory {
	private CmdConnector cmdConnector;
	
	public Directory() {
		cmdConnector = new CmdConnector();
	}
	public String getLastModifiedDate(File file) {
		String pattern = "yyyy-MM-dd  aa hh:mm";
		SimpleDateFormat simpleDateFormat = new SimpleDateFormat(pattern);
		long lastModified = file.lastModified();
		Date lastModifiedDate = new Date(lastModified);
		
		return simpleDateFormat.format(lastModifiedDate);
	}
	public String getFileSize(File file) {
		String fileSize = "";
		
		if(file.isDirectory()) {
			return fileSize;
		}
		
		try {
			fileSize = Long.toString(Files.size(Paths.get(file.getPath())));
		} catch (IOException e) {
			e.printStackTrace();
		}
		return String.format("%15s", fileSize);
	}
	public String getCategory(File file) {
		if (file.isDirectory()) {
			return "    <DIR>      ";
        }
		return "";
	}
	public DirectoryInformation getDirectoryInformation(File file) {
		DirectoryInformation directoryInformation = new DirectoryInformation();
		String lastModifiedDate = getLastModifiedDate(file);;
		String category = getCategory(file);
		String fileSize = getFileSize(file);
		String fileName = file.getName();
		
    	directoryInformation.setDirectoryInformation(
    			lastModifiedDate, category, fileSize, fileName);
    	return directoryInformation;
	}
	public void printDirectory(String path) {
		File file = new File(path); 
        File[] fileList = file.listFiles(); 
        ArrayList<DirectoryInformation> directory =
        		new ArrayList<DirectoryInformation>();

        Arrays.sort(fileList);
		System.out.println(cmdConnector.getCmdExecutionResult("vol c:"));  //디스크 일련번호
		
        for(int i = 0; i < fileList.length; i++) { 
        	if(fileList[i].isHidden()) continue;
        	directory.add(getDirectoryInformation(fileList[i]));
        	System.out.print(getDirectoryInformation(fileList[i]));
        }
        
        long directorySize = 0;
        long fileSize = 0; 
        int numberoOfDirectory = 0;
        int numberoOfFile = 0;
        
        for(int index = 0; index < directory.size(); index++) {
        	if(directory.get(index).getCategory().contains("DIR")) {
        		//directorySize += Long.parseLong(directory.get(index).getFileSize().trim());
        		numberoOfDirectory++;
        	}
        	else {
        		fileSize += Long.parseLong(directory.get(index).getFileSize().trim());
        		numberoOfFile++;
        	}
        }
        
        System.out.println(String.format("%30s", numberoOfFile + "개 파일    ") + String.format("%30s", fileSize + "바이트    "));
        System.out.println(String.format("%30s", numberoOfDirectory + "개 디렉터리  "));
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
