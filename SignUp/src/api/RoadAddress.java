package api;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.nio.charset.Charset;
import java.util.ArrayList;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class RoadAddress {
	
	private String searchRoadAddress(String roadAddress) {
		String url = "http://dapi.kakao.com/v2/local/search/address.json?size=30&query=";
	    String userInformation = "KakaoAK e7340e1df71abf4e5a6ef4aa3fcb7fbb"; 
	    String inputLine;
        StringBuffer response = new StringBuffer();
	    
	    URL obj;
		
        try{
            String address = URLEncoder.encode(roadAddress, "UTF-8");//"판교역로 235", "UTF-8");
            
            obj = new URL(url + address);
			
            HttpURLConnection con = (HttpURLConnection)obj.openConnection();
            
            con.setRequestMethod("GET");
            con.setRequestProperty("Authorization",userInformation);
            con.setRequestProperty("content-type", "application/json");
            con.setDoOutput(true);
            con.setUseCaches(false);
            con.setDefaultUseCaches(false);
			
            Charset charset = Charset.forName("UTF-8");
            BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream(), charset));
            
            while ((inputLine = in.readLine()) != null) {
                response.append(inputLine);
            }
            
            
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        return response.toString();
	}
	
	private String getRoadAddress(JSONObject searchObject) {
        StringBuilder roadAdrress = new StringBuilder();
		JSONObject addressObject = (JSONObject) searchObject.get("road_address");    //도로명 주소 정보
    	
    	roadAdrress.append(String.format("[%s] ", addressObject.get("zone_no")));    //우편번호
    	roadAdrress.append(addressObject.get("address_name"));                       //도로명 주소
    	roadAdrress.append("(" + addressObject.get("region_3depth_name"));           //읍/면/동
    	if(addressObject.get("building_name").equals("")) {                          //아파트 이름
    		roadAdrress.append(")");
    	}
    	else {
    		roadAdrress.append(", " + addressObject.get("building_name") + ")");
    	}
    	
    	return roadAdrress.toString();
	}
	
	public ArrayList<String> getSearchResult(String searchInput) {
		JSONParser jsonParser = new JSONParser();
        JSONObject jsonObject;
        JSONObject searchObject;
    	JSONArray searchReulst = new JSONArray();
        ArrayList<String> roadAdrress = new ArrayList<String>();
        
        String result = searchRoadAddress(searchInput);   //도로명 주소 검색결과
        
		try {
			jsonObject = (JSONObject) jsonParser.parse(result);
	        searchReulst = (JSONArray) jsonObject.get("documents");
		} catch (ParseException e) {
			e.printStackTrace();
		}
        
        
        for(int i=0; i<searchReulst.size(); i++){
        	searchObject = (JSONObject) searchReulst.get(i);
        	roadAdrress.add(getRoadAddress(searchObject));
        }
        
		return roadAdrress;
	}
}
