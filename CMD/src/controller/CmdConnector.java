package controller;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;

public class CmdConnector {
	public String getCmdExecutionResult(String command) {
		Process process = null;
		BufferedReader reader;
        String line;
        StringBuffer sb = new StringBuffer();
        
		try {
			process = Runtime.getRuntime().exec("cmd /c " + command);
			reader = new BufferedReader(new InputStreamReader(process.getInputStream(), "MS949"));
			while ((line = reader.readLine()) != null) {
			    sb.append(line);
			    sb.append("\n");
			}
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
        return sb.toString();
	}
}
