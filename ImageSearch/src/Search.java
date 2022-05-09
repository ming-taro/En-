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
		URL url = null;
	    String readLine = null;
	    StringBuilder buffer = null;
	    BufferedReader bufferedReader = null;
	    HttpURLConnection connection = null;
	    	
	    int connTimeout = 5000;
	    int readTimeout = 3000;
			
	    String apiUrl = "https://dapi.kakao.com/v2/search/image?sort=accuracy&query='"+ searchWord +"'&size=10";			
	    url = new URL(apiUrl);
        connection = (HttpURLConnection)url.openConnection();
        
        connection.setRequestMethod("GET");	     
        connection.setConnectTimeout(connTimeout);
        connection.setReadTimeout(readTimeout);
        connection.setRequestProperty("Authorization", "KakaoAK e7340e1df71abf4e5a6ef4aa3fcb7fbb");
        connection.setRequestProperty("content-type", "application/json; charset=utf-8");
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
			
		/*JFrame frame = new JFrame();
		frame.setBounds(10,10,800,800);
		frame.setVisible(true);
		JPanel panel = new JPanel(new GridLayout(2,5,5,5));
		for(int i=0; i<10; i++) {
			ImageIcon image = new ImageIcon(new URL(urlList.get(i)));
			JLabel label = new JLabel("", image, JLabel.CENTER);
			panel.add(label, BorderLayout.CENTER );
		}
		panel.setBounds(0,0,800,800);
		frame.setContentPane(panel);*/

	}
}
