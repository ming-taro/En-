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
		return String.format("%10s", fileSize);
	}
	public String getCategory(File file) {
		if (file.isDirectory()) {
			return "  <DIR>   ";
        }
		return "";
	}
	public void printDirectory(String path) {
		File file = new File(path); 
        File[] fileList = file.listFiles(); 
		DirectoryInformation directoryInformation;
		ArrayList<DirectoryInformation> directory = new ArrayList<DirectoryInformation>();

		
        Arrays.sort(fileList);
		System.out.println(cmdConnector.getCmdExecutionResult("vol c:"));  //디스크 일련번호

        for( int i = 0; i < fileList.length; i++ ) { 
        	directoryInformation = new DirectoryInformation();
        	
        	directoryInformation.setLastModifiedDate(getLastModifiedDate(fileList[i]));  //마지막 수정일
        	directoryInformation.setCategory(getCategory(fileList[i]));
            directoryInformation.setFileSize(getFileSize(fileList[i]));
            directoryInformation.setFileName(fileList[i].getName());
            
    		System.out.print(directoryInformation);
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
