import java.awt.BorderLayout;
import java.awt.GridLayout;
import java.io.*;
import java.net.*;
import java.net.http.HttpRequest;
import java.util.ArrayList;

import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import netscape.javascript.JSObject;

public class Search {
	private String searchWord;
	private ArrayList<String> urlList;
	public Search() {
        urlList = new ArrayList<String>();
	}
	public ArrayList<String> getUrlList(){
		return urlList;
	}
	public void searchImage(String searchWord) throws IOException, ParseException {
		URL url;
	    String readLine;
	    StringBuilder buffer;
	    BufferedReader bufferedReader;
	    HttpURLConnection connection;
	    	
	    int connTimeout = 5000;
	    int readTimeout = 3000;
			
	    String apiUrl = "https://dapi.kakao.com/v2/search/image?query='"+ searchWord +"'&size=30";			
	    url = new URL(apiUrl);
        connection = (HttpURLConnection)url.openConnection();
        
        connection.setRequestMethod("GET");	     
        connection.setConnectTimeout(connTimeout);
        connection.setReadTimeout(readTimeout);
        connection.setRequestProperty("Authorization", "KakaoAK e7340e1df71abf4e5a6ef4aa3fcb7fbb");
        connection.setRequestProperty("content-type", "application/json; charset=utf-8;");
        connection.setRequestProperty("Accept", "application/json; charset=utf-8;");
        connection.setDoOutput(true);
        
        buffer = new StringBuilder();
        
        JSONParser jsonParse;
        JSONObject data;
        JSONArray array;
        JSONObject document;
        
        if(connection.getResponseCode() == HttpURLConnection.HTTP_OK) 
        {
            bufferedReader = new BufferedReader(new InputStreamReader(connection.getInputStream(),"UTF-8"));
            
            while((readLine = bufferedReader.readLine()) != null) 
    	    {
            	jsonParse = new JSONParser();
            	data = (JSONObject)jsonParse.parse(readLine);
            	array = (JSONArray)data.get("documents");
            	
            	for(int i=0; i<30; i++) {
            		document = (JSONObject)array.get(i);
	            	buffer.append(document.get("image_url")).append("\n");
	            	urlList.add(document.get("image_url").toString());
            	}
            	
            }
        }
        else 
        {
            buffer.append("code : ");
            buffer.append(connection.getResponseCode()).append("\n");
            buffer.append("message : ");
            buffer.append(connection.getResponseMessage()).append("\n");
        }

	}
}
