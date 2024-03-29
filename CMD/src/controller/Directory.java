package controller;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import dto.DirectoryInformation;
import utility.Constants;

public class Directory {
	private CmdConnector cmdConnector;
	
	public Directory() {
		cmdConnector = new CmdConnector();
	}
	private String getLastModifiedDate(File file) {
		String pattern = "yyyy-MM-dd  aa hh:mm";
		SimpleDateFormat simpleDateFormat = new SimpleDateFormat(pattern);
		long lastModified = file.lastModified();
		Date lastModifiedDate = new Date(lastModified);
		
		return simpleDateFormat.format(lastModifiedDate);  //마지막 수정일
	}
	private String getFileSize(File file) {
		String fileSize = "";
		
		if(file.isDirectory()) {  //디렉토리는 크기를 구하지 않음
			return fileSize;
		}
		
		try {
			fileSize = Long.toString(Files.size(Paths.get(file.getPath())));         //파일 크기(long)
			fileSize = fileSize.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","); //천 단위 콤마 표시
		} catch (IOException e) {
			e.printStackTrace();
		}
		return String.format("%15s", fileSize);
	}
	private String getCategory(File file) {
		if (file.isDirectory()) {
			return "    <DIR>      ";       //디렉토리인 경우 출력시 파일유형에 <DIR>표시
        } 
		return "";
	}
	private DirectoryInformation getDirectoryInformation(File file) {
		DirectoryInformation directoryInformation = new DirectoryInformation();
		String lastModifiedDate = getLastModifiedDate(file);;
		String category = getCategory(file);
		String fileSize = getFileSize(file);
		String fileName = file.getName();
		
    	directoryInformation.setDirectoryInformation(
    			lastModifiedDate, category, fileSize, fileName);
    	return directoryInformation;
	}
	private int getNumberOfWord(String sentence, String word) {
		int lengthBeforeChange = sentence.length();
		int lengthAfterChange = sentence.replace(word, "").length();
		
		return lengthBeforeChange - lengthAfterChange;
	}
	private void printNofile(String currentPath, String pathEntered) {
		if(pathEntered.indexOf("\\") == -1) {
			System.out.println(" " + currentPath + " 디렉터리\n");
		}
		else if(getNumberOfWord(pathEntered, "\\") == 1) {
			System.out.println("C:\\ 디렉터리\n");
		}
		System.out.println("파일을 찾을 수 없습니다.");
	}
	private void printDirectorySize(ArrayList<DirectoryInformation> directory, File file) {
		long fileSize = 0; 
		String size;
        int numberoOfDirectory = 0;
        int numberoOfFile = 0;
        
        for(int index = 0; index < directory.size(); index++) {
        	System.out.print(directory.get(index));                      //디렉터리 정보 출력
        	if(directory.get(index).getCategory().contains("DIR")) {
        		numberoOfDirectory++;
        	}
        	else {
        		size = directory.get(index).getFileSize().trim().replace(",", "");
        		fileSize += Long.parseLong(size);
        		numberoOfFile++;
        	}
        }
        
        System.out.print(String.format("%25s", numberoOfFile + "개 파일    "));
        System.out.println(String.format("%30s", fileSize + "바이트")
        		.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","));
        System.out.print(String.format("%25s", numberoOfDirectory + "개 디렉터리  "));
        System.out.println(String.format("%30s", file.getUsableSpace() + " 바이트  남음")
        		.replaceAll(Constants.THOUSAND_SEPARATOR_REGEX, ","));
	}
	private void printDirectory(String currentPath, String pathEntered) {
		File file = new File(pathEntered); 
        File[] fileList = file.listFiles(); 
        ArrayList<DirectoryInformation> directory =
        		new ArrayList<DirectoryInformation>();

        if(fileList == null && getNumberOfWord(pathEntered, "\\") > 1) {   //경로가 잘못된 경우
        	System.out.println("지정된 파일을 찾을 수 없습니다.");
			return;
        }
        
		System.out.println(cmdConnector.getCmdExecutionResult("vol c:"));  //디스크 일련번호
		
		if(fileList == null) {
			printNofile(currentPath, pathEntered);
			return;
		}
		
		System.out.println(" " + pathEntered + " 디렉터리\n");
		
        for(int i = 0; i < fileList.length; i++) { 
        	if(fileList[i].isHidden()) continue;
        	directory.add(getDirectoryInformation(fileList[i]));
        }
        
        printDirectorySize(directory, file);
	}
	public void executeCommand(String currentPath, String command) {
		int beginIndex = command.indexOf(' ') + 1;
		String pathEntered = command.substring(beginIndex).trim();    //copy 다음에 이어지는 명령문		

		if(command.equals("dir")) {                       //'dir'입력시 현재 디렉토리 목록 출력
			printDirectory(currentPath, currentPath);
		}
		else {                                            //'dir'뒤에 입력한 디렉토리 경로의 목록 출력
			printDirectory(currentPath, pathEntered);
		}
	}
	public void execute(String currentPath, String command) {
		executeCommand(currentPath, command);
	}
}
