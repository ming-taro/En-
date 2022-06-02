package main;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.*;
import java.nio.charset.Charset;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

import ui.PanelSwitcher;

public class SignUp {

	public static void main(String[] args) {
		//PanelSwitcher panelSwitcher = new PanelSwitcher();
		String GEOCODE_URL="http://dapi.kakao.com/v2/local/search/address.json?size=30&query=";
	    String GEOCODE_USER_INFO="KakaoAK e7340e1df71abf4e5a6ef4aa3fcb7fbb"; 
	    
	    URL obj;
		
        try{
            //인코딩한 String을 넘겨야 원하는 데이터를 받을 수 있다.
            String address = URLEncoder.encode("판교역로 235", "UTF-8");
            
            obj = new URL(GEOCODE_URL+address);
			
            HttpURLConnection con = (HttpURLConnection)obj.openConnection();
            
            //get으로 받아오면 된다. 자세한 사항은 카카오개발자센터에 나와있다.
            con.setRequestMethod("GET");
            con.setRequestProperty("Authorization",GEOCODE_USER_INFO);
            con.setRequestProperty("content-type", "application/json");
            con.setDoOutput(true);
            con.setUseCaches(false);
            con.setDefaultUseCaches(false);
			
            Charset charset = Charset.forName("UTF-8");
            BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream(), charset));
            
            String inputLine;
            StringBuffer response = new StringBuffer();
            
            while ((inputLine = in.readLine()) != null) {
                response.append(inputLine);
            }
            //response 객체를 출력해보자
            System.out.println(response.toString());
     		
            JSONParser jsonParser = new JSONParser();
            JSONObject jsonObject = (JSONObject) jsonParser.parse(response.toString());
            JSONArray bookInfoArray = (JSONArray) jsonObject.get("documents");
            
            System.out.println(bookInfoArray.size());
            for(int i=0; i<bookInfoArray.size(); i++){
            	JSONObject bookObject = (JSONObject) bookInfoArray.get(i);
            	//System.out.println("address " + i + ":"+bookObject.get("address_name"));
            	JSONObject addressObject = (JSONObject) bookObject.get("road_address");
            	System.out.println("address " + i + ":"+addressObject.get("address_name"));
            	System.out.println("        " + i + ":"+addressObject.get("region_3depth_name"));
            	System.out.println("        " + i + ":"+addressObject.get("building_name"));
            	System.out.println("        " + i + ":"+addressObject.get("zone_no"));
            }
 
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
