import java.io.*;
import java.net.*;
import java.net.http.HttpRequest;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import netscape.javascript.JSObject;

public class Search {

	public void searchImage() throws IOException {
		
		URL url = null;
	    String readLine = null;
	    StringBuilder buffer = null;
	    BufferedReader bufferedReader = null;
	    BufferedWriter bufferedWriter = null;
	    HttpURLConnection connection = null;
	    	
	    int connTimeout = 5000;
	    int readTimeout = 3000;
			
	    String apiUrl = "https://dapi.kakao.com/v2/search/image?query='dog'&size=10";			
			
	    try 
	    {
	        url = new URL(apiUrl);
	        connection = (HttpURLConnection)url.openConnection();
	        
	        connection.setRequestMethod("GET");	     
	        connection.setConnectTimeout(connTimeout);
	        connection.setReadTimeout(readTimeout);
	        connection.setRequestProperty("Authorization", "KakaoAK e7340e1df71abf4e5a6ef4aa3fcb7fbb");
	        connection.setRequestProperty("content-type", "application/json");
	        connection.setRequestProperty("Accept", "application/json;");
	        connection.setDoOutput(true);
	        
	        buffer = new StringBuilder();
	        if(connection.getResponseCode() == HttpURLConnection.HTTP_OK) 
	        {
	            bufferedReader = new BufferedReader(new InputStreamReader(connection.getInputStream(),"UTF-8"));
	            
	            while((readLine = bufferedReader.readLine()) != null) 
	    	    {
	            	JSONParser jsonParse = new JSONParser();
	            	JSONObject data = (JSONObject)jsonParse.parse(readLine);
	            	JSONArray array = (JSONArray)data.get("documents");//  get("document");
	            	
	            	for(int i=0; i<10; i++) {
	            		JSONObject document = (JSONObject)array.get(i);
		            	buffer.append(document.get("image_url")).append("\n");
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
	    catch(Exception ex) 
	    {
	        ex.printStackTrace();
	    }
	    finally 
	    {
	        try 
	        {
	            if (bufferedWriter != null) { bufferedWriter.close(); }
	            if (bufferedReader != null) { bufferedReader.close(); }
	        }
	        catch(Exception ex) 
	        { 
	            ex.printStackTrace();
	        }
	    }
			
			
	   System.out.println("°á°ú : " + buffer.toString());
	   

	}
}
