// This is the main DLL file.

#include "stdafx.h"
#include "processWeb.h"
#include <opencv2/opencv.hpp>
#include <time.h>
#include <iostream>
#include <fstream>
using namespace std;
using namespace processWeb;
int offSet = 0;
Mat imgC1;
Mat imgC2;
Mat imgC3;
Mat combinedImg;
int out = 0;
int sheetStartX = 0;
int outArr[7] = { 0,0,0,0,0,0,0 };
int frameCount = 0;
Rect boundingRectPriv;
string path_s = "C:/AddInnovations/";
struct defectPoint
{
	Point tl{};
	Point br{};
	float defectArea{};
	int defectCat{};
};
vector<defectPoint> returnDefPoints;
//bitmap to mat
Mat BitmapToMat(System::Drawing::Bitmap^ bitmap)
{

	System::Drawing::Rectangle blank = System::Drawing::Rectangle(0, 0, bitmap->Width, bitmap->Height);
	System::Drawing::Imaging::BitmapData^ bmpdata = bitmap->LockBits(blank, System::Drawing::Imaging::ImageLockMode::ReadWrite, bitmap->PixelFormat);
	if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format8bppIndexed) {
		//tmp = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_8U, 1);
		//tmp->imageData = (char*)bmData->Scan0.ToPointer();
		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC1, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;
	}

	else if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format24bppRgb) {

		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC3, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;
	}

	Mat returnMat = (Mat::zeros(640, 480, CV_8UC1));
	//   bitmap->UnlockBits(bmData);
	return returnMat;
	//   return cvarrToMat(tmp);
}

std::string getpath()
{
	try {

		string  model_name_in_file_path = path_s + "mod.txt";
		std::ifstream file(model_name_in_file_path);
		std::string str;
		while (std::getline(file, str)) {
			// Process str
			std::cout << str << endl;

			break;
		}
		file.close();
		return str;
	}
	catch (exception e) {
		cout << "Failed reading file\n" << e.what() << endl;;
	}
}//

// main code

//System::Drawing::Bitmap^ processWeb::Class1::processAll(System::Drawing::Bitmap^  bmp1,System::Drawing::Bitmap^  bmp2,System::Drawing::Bitmap^  bmp3)
//{clock_t begin, end;
//	double time_spent;
//	begin = clock();
//
//
//	imgC1=BitmapToMat(bmp1);
//	imgC2=BitmapToMat(bmp2);
//	imgC3=BitmapToMat(bmp3);
//	
//	combinedImg=Mat::zeros(imgC1.rows,imgC1.cols*3,imgC1.type());
//
//	imgC1.copyTo(combinedImg(Rect(0,0,imgC1.cols,imgC1.rows)));
//	imgC2.copyTo(combinedImg(Rect(imgC1.cols,0,imgC1.cols,imgC1.rows)));
//	imgC3.copyTo(combinedImg(Rect(imgC1.cols*2,0,imgC1.cols,imgC1.rows)));
//
//	Mat combinedImgColor;
//	if(combinedImg.channels()<3)
//		cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
//	else
//		combinedImgColor=combinedImg.clone();
//
//	//threshold(combinedImg,combinedImg,8,255,CV_THRESH_BINARY);
//	if (combinedImg.channels()>2)
//		cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);
//
//	Mat m1,m2;
//	
//	if(combinedImg.channels()<3)
//		cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
//
//
//	for (int row=0;row<(combinedImg.rows-19);row=row+20)
//		{
//			for(int col=0;col<(combinedImg.cols-19);col=col+20)
//			{Rect window=Rect(col,row,20,20);
//			m1=combinedImg(window);
//	
//			 Scalar m = mean(m1);
//			 float myMAtMean = m.val[0];
//		/*	  Scalar r = mean(m2);
//			 float myMAtMean_roi = r.val[0];*/
//		// cout<<"myMAtMean_roi"<<m <<endl;
//		 //cout<<"myMAtMean"<<r<<endl;   
//		
//			//Scalar mssim=getMSSIM(m1,m2);
//			//cout<<"mssim::"<<mssim[0]<<endl;
//		//	if (mssim[0]<0.1)											
//				if  (myMAtMean>10 && myMAtMean<256)
//				{	rectangle(combinedImgColor,Rect(window.x,window.y,window.width,window.height),Scalar(0,255,0),1);
//				//cout<<"myMAtMean_roi cond=="<<myMAtMean_roi <<endl;
//					out=1;
//				}
//				else
//				{//rectangle(combinedImgColor,Rect(window.x,window.y,window.width,window.height),Scalar(0,0,255),2);
//					//defCount+=1;
//				}
//
//			}
//		}
//
//
//	
//
//	System::Drawing::Bitmap^ returnImg=MatToBitmap(combinedImgColor );
//	out=1;
//
//		end = clock();
//	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
//	return returnImg;
//
//}

////999999999999999999999999999999999
Mat inImgTh;
Mat imgGrey;
Mat b_hist;
Mat readTempl;
int thresh = 100;
Mat inImage;
RNG rng(12345);
//bool Positive = true;
int counter = 0;
float percentage;
float percentageRead;
//--------------------------------
vector<vector<Point> > read_cont;
//-------------------------------
int readFlag = 0;
Mat grad;
Mat retImgCS;
int trimWidth;
void write_percentage(float pc)
{

	FileStorage storage(path_s + "percentage.xml", FileStorage::WRITE);
	storage << "img" << pc;
	storage.release();
}

float read_percentage()
{
	float readPc;
	FileStorage storageR(path_s + "percentage.xml", FileStorage::READ);
	storageR["img"] >> readPc;
	storageR.release();
	return readPc;
}


void write_FinalConts(vector<vector<cv::Point> > vvP)
{
	FileStorage fs(path_s + "SaveContours.xml", FileStorage::WRITE);
	fs << "Contours" << "[";

	for (int i = 0; i < vvP.size(); ++i)
	{

		fs << "[:";

		for (int j = 0; j < vvP[i].size(); ++j)
		{
			fs << "[" << vvP[i][j].x << vvP[i][j].y << "]";
		}
		fs << "]";
	}
	fs << "]";

	/*fs.release();*/
}

vector<vector<cv::Point>> FinalConts_readXML()
{
	vector<vector<cv::Point>> v;
	FileStorage fs(path_s + "SaveContours.xml", FileStorage::READ);
	if (!fs.isOpened())
	{
		cout << "Unable to open: " << endl;
	}
	FileNode Contours = fs["Contours"];
	for (FileNodeIterator itData = Contours.begin(); itData != Contours.end(); ++itData)
	{
		vector<cv::Point>temp;
		FileNode pts = *itData;
		for (FileNodeIterator itPts = pts.begin(); itPts != pts.end(); ++itPts)
		{
			FileNode pt = *itPts;
			cv::Point point;
			FileNodeIterator itPt = pt.begin();
			point.x = *itPt; ++itPt;
			point.y = *itPt; ++itPt;

			temp.push_back(point);
		}
		v.push_back(temp);
	}

	return v;
}
int processWeb::Class1::loadData()
{
	path_s = "C:/Database/" + getpath() + "/";
	read_cont.clear();
	read_cont = FinalConts_readXML();

	return 1;
}
//99999999999999999999999999999999999
System::Drawing::Bitmap^ processWeb::Class1::processAll(System::Drawing::Bitmap^ bmp1)
{
	clock_t begin, end;
	double time_spent;
	begin = clock();
	/*
	 for (int k=0 ; k<7;k++)
	 {
	 outArr[k]=0;
	 }
*/
/*imgC1=BitmapToMat(bmp1);
imgC2=BitmapToMat(bmp2);
imgC3=BitmapToMat(bmp3);      /////////////////// Code commented because C# will provide merged images.

combinedImg=Mat::zeros(imgC1.rows,(imgC1.cols)*3-(overlapC1C2Prop)*2 -(overlapC2C3Prop) ,imgC1.type());
int startPosC2=imgC1.cols-overlapC1C2Prop;
int startPosC3=(imgC1.cols)*2-(overlapC1C2Prop*2)-(overlapC2C3Prop);
imgC1.copyTo(combinedImg(Rect(0,0,imgC1.cols,imgC1.rows)));
imgC2.copyTo(combinedImg(Rect(startPosC2,0,imgC1.cols,imgC1.rows)));
imgC3.copyTo(combinedImg(Rect(startPosC3,0,imgC1.cols,imgC1.rows)));*/
//----------------save images
//imwrite("D:/imgs/c1/1.bmp",imgC1);
//imwrite("D:/imgs/c2/1.bmp",imgC2);
//imwrite("D:/imgs/c3/1.bmp",imgC3);
//string  path ="D:/imgs/comb/" +to_string ( frameCount) +".bmp";
//imwrite(path,combinedImg);
	combinedImg = BitmapToMat(bmp1);
	frameCount += 1;
	//---------------------------
	Mat combinedImgColor;
	if (combinedImg.channels() < 3)
		cvtColor(combinedImg, combinedImgColor, COLOR_GRAY2BGR);
	else
		combinedImgColor = combinedImg.clone();

	//threshold(combinedImg,combinedImg,thresholdV ,255,CV_THRESH_BINARY_INV);
	//if (combinedImg.channels()>2)
	//	cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);


	////cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
	//Mat m1,m2;

	//if(combinedImg.channels()<3)
	//	cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);

	//line(combinedImgColor,Point(sheetWidthProp,0),Point(sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(2*sheetWidthProp,0),Point(2*sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(3*sheetWidthProp,0),Point(3*sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(4*sheetWidthProp,0),Point(4*sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(5*sheetWidthProp,0),Point(5*sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(6*sheetWidthProp,0),Point(6*sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),4);
	//line(combinedImgColor,Point(sheetWidthProp,0),Point(sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),2);

//------------------------find cont
	//  vector<vector<Point> > contours;
 //   vector<Vec4i> hierarchy;
	//RotatedRect RR;
 // findContours(combinedImg, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
 // for (int i = 0; i < contours.size(); i++) 
 // {  
	//  RR= minAreaRect(contours[i]);
	//  double contArea=contourArea(contours[i]);
	//  if (contArea>=8500  )
	//  drawContours(combinedImgColor, contours, i, Scalar(0, 255, 0), 3, 8, hierarchy, 0, Point());
	// if(contArea<800 &&  contArea>300)
	//  {int position=(int)((int)RR.center.x/sheetWidthProp);
	//	  cout<<"position::"<<position<<endl;
	//  if (position<7)
	//  { //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
	//
	//	 outArr[position]+=1;
	//  }
	//  
	//  }
 // }
 // 
//------------------------------
		/*out1Prop=outArr[0];
		out2Prop=outArr[1];
		out3Prop=outArr[2];
		out4Prop=outArr[3];
		out5Prop=outArr[4];
		out6Prop=outArr[5];
		out7Prop=outArr[6];

*/


//-----------------------------



	out = 1;

	end = clock();
	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
	cout << "Proc Time  in sec " << time_spent << endl;
	return bmp1;

}
System::Drawing::Bitmap^ processWeb::Class1::processOne(System::Drawing::Bitmap^ bmp1)
{
	clock_t begin, end;
	double time_spent;
	begin = clock();
	Mat imgC1 = BitmapToMat(bmp1);

	//cout<<"imgC channels"<<imgC1.channels()<<"  size  ::"<<imgC1.size()<<endl;
	//combinedImg = Mat::zeros(imgC1.rows, imgC1.cols, imgC1.type());
	combinedImg = BitmapToMat(bmp1);
	//imshow("Res Proc1",imgC1);
//waitKey(2);
	imgC1.copyTo(combinedImg(Rect(0, 0, imgC1.cols, imgC1.rows)));
	threshold(combinedImg, combinedImg, 8, 255, THRESH_BINARY);
	if (combinedImg.channels() > 2)
		cvtColor(combinedImg, combinedImg, COLOR_BGR2GRAY);

	Mat m1, m2;
	Mat combinedImgColor;
	if (combinedImg.channels() < 3)
		cvtColor(combinedImg, combinedImgColor, COLOR_GRAY2BGR);


	for (int row = 0; row < (combinedImg.rows - 19); row = row + 20)
	{
		for (int col = 0; col < (combinedImg.cols - 19); col = col + 20)
		{
			Rect window = Rect(col, row, 20, 20);
			m1 = combinedImg(window);

			Scalar m = mean(m1);
			float myMAtMean = m.val[0];
			/*	  Scalar r = mean(m2);
				 float myMAtMean_roi = r.val[0];*/
				 // cout<<"myMAtMean_roi"<<m <<endl;
				  //cout<<"myMAtMean"<<r<<endl;   

					 //Scalar mssim=getMSSIM(m1,m2);
					 //cout<<"mssim::"<<mssim[0]<<endl;
				 //	if (mssim[0]<0.1)											
			if (myMAtMean > 10 && myMAtMean < 256)
			{
				rectangle(combinedImgColor, Rect(window.x, window.y, window.width, window.height), Scalar(0, 255, 0), -1);
				//cout<<"myMAtMean_roi cond=="<<myMAtMean_roi <<endl;
				out = 1;
			}
			else
			{//rectangle(combinedImgColor,Rect(window.x,window.y,window.width,window.height),Scalar(0,0,255),2);
				//defCount+=1;
			}

		}
	}





	out = 1;

	end = clock();
	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
	//	cout<<"Stitch Time  in sec "<< time_spent <<endl;

	return bmp1;

}
System::Drawing::Bitmap^ processWeb::Class1::processAllBack(System::Drawing::Bitmap^ bmp1)
{
	clock_t begin, end;
	counter = 0;
	double time_spent;
	begin = clock();

	for (int k = 0; k < 7; k++)
	{
		outArr[k] = 0;
	}




	combinedImg = BitmapToMat(bmp1);
	int startPosC2 = imgC1.cols - overlapC1C2Prop;
	int startPosC3 = (imgC1.cols) * 2 - (overlapC1C2Prop * 2) - (overlapC2C3Prop);

	//----------------save images
	//imwrite("D:/imgs/c1/1.bmp",imgC1);
	//imwrite("D:/imgs/c2/1.bmp",imgC2);
	//imwrite("D:/imgs/c3/1.bmp",imgC3);
	//imwrite("D:/imgs/comb/1.bmp",combinedImg);
	//---------------------------
	Mat combinedImgColor;
	if (combinedImg.channels() < 3)
		cvtColor(combinedImg, combinedImgColor, COLOR_GRAY2BGR);
	else
		combinedImgColor = combinedImg.clone();
	//------------------------------------------------------------------------------
	/*threshold(combinedImg,combinedImg,thresholdV ,255,CV_THRESH_BINARY_INV);
	if (combinedImg.channels()>2)
		cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);*/

		//---------------------------------------------------------------###########################################################
	inImage = combinedImg.clone();//imread("C:/Users/Pradeep Kumar/Desktop/New folder (3)/sand paper/c1/1.bmp");
   // 		string  path ="D:/imgs/comb/" +to_string ( frameCount) +".bmp";
   //imwrite(path,combinedImg);
	frameCount += 1;
	//cout<<"inImg channels::"<<inImage.channels()<<endl;

	if (inImage.channels() > 2)
		cvtColor(inImage, imgGrey, COLOR_BGR2GRAY);
	else
		imgGrey = inImage.clone();

	//imshow("inputimg",inImage);
	threshold(imgGrey, inImgTh, th1Prop, 255, THRESH_BINARY);

	Mat canny_output;
	inRange(imgGrey, th2Prop, th3Prop, canny_output);  //15  ,210
	canny_output = 255 - canny_output;

	//imshow("thresimg",inImgTh);

	Mat res;
	resize(canny_output, res, Size(), 0.15, 0.15);
	//imshow("inrange",res);
	//resize(inImage,res,Size(),0.3,0.3);
	//imshow("input img",res);

	//Mat res1;
	//resize(inImgTh,res1,Size(),0.3,0.3);
	//imshow("thres img",res1);


	vector<vector<Point> > contours;
	//vector<vector<int> > vvP.size();

	vector<vector<Point> > vvP;

	vector<Vec4i> hierarchy;
	/// Find contours
	findContours(inImgTh, contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE, Point(0, 0));

	/// Draw contours
	Mat drawing = Mat::zeros(inImgTh.size(), CV_8UC3);
	Mat drawing1 = Mat::zeros(inImgTh.size(), CV_8UC3);



	Mat img = combinedImgColor.clone();
	int largest_area = 0;
	int largest_contour_index = 0;
	Rect bounding_rect;




	for (int i = 0; i < contours.size(); i++)
	{
		double contAr = contourArea(contours[i], true);
		RotatedRect r1 = minAreaRect(contours[i]);
		int d1 = r1.size.height;
		int d2 = r1.size.width;
		int height = max(d1, d2);
		int width = min(d1, d2);


		double a = contourArea(contours[i], false);
		if (a > largest_area)
		{
			largest_area = a;
			largest_contour_index = i;
			bounding_rect = boundingRect(contours[i]);

		}

		if (contAr > th4Prop)  //14000
		{
			//cout<<"con area:"<<i<<": "<<contAr<<endl;
			 /*cout<< "width:" << width << endl;
			 cout<< "height:" << height <<endl; */


			 //if(height>210 && height<350)  

			int numOfPixels = contours[i].size();
			bool edgecont = false;
			for (int j = 0; j < contours[i].size(); j++)
			{
				Point coord = contours[i][j];
				//  cout  << "coordinates: "<<i  <<"cont Point :"<<j<<" x = " << coord.x<< " y = "<<coord.y << endl;

				if (coord.y<5 || coord.y>(inImgTh.rows - 5) || coord.x<5 || coord.x>(inImgTh.cols - 5))
				{
					edgecont = true;
				}


			}
			if (edgecont == false)
			{
				//   drawContours(drawing, contours, i, Scalar (0,0,255), 3 ,8,  hierarchy, 0, Point() );


				vvP.push_back(contours[i]);
			}
		}

	}

	// drawContours( drawing, contours,largest_contour_index, Scalar(0,0,255), CV_FILLED, 8, hierarchy );
	rectangle(img, bounding_rect, Scalar(0, 0, 255), 8, 8, 0);
	sheetStartX = bounding_rect.x;
	cout << " initial x cordinate::::" << bounding_rect.x << endl;

	cout << "last x cordinate===" << bounding_rect.x + bounding_rect.width << endl;
	sheetWidthProp = bounding_rect.width / 7;
	//.................;...........................percentage............................................................ 
	Rect sheetRect = Rect(bounding_rect.x + 100, bounding_rect.y, bounding_rect.width - 200, bounding_rect.height);
	int TotalNumberOfPixels = sheetRect.width * sheetRect.height;
	int ZeroPixels = TotalNumberOfPixels - countNonZero(inImgTh(sheetRect));
	//cout<<"The number of pixels black:::::: "<<ZeroPixels<<endl;
   //cout<< "total no. of pixels:: "<<TotalNumberOfPixels<<endl;
   //cout<< "total no. of pixels white::::::: "<<countNonZero(inImgTh)<<endl;

	//  percentfthresh_callback( );
	percentage = ((float)ZeroPixels / (float)TotalNumberOfPixels) * 100;

	// cout<<"percentage of black:::: "<<percentage<<endl;



//......................................................................................

	contours.clear();
	hierarchy.clear();
	int vvpBfrcont2 = vvP.size();
	findContours(canny_output(sheetRect), contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE, Point(0, 0));
	for (int i = 0; i < contours.size(); i++)
	{
		if (contourArea(contours[i]) > 50)
			vvP.push_back(contours[i]);
	}



	//...........................................................read & write.................................................... 
	if (trainTESTProp == true)
	{
		write_percentage(percentage);
		//cout<< "in.cont=="<<percentage.size()<<endl;
		// percentagef=percentage;
		write_FinalConts(vvP);
		read_cont.clear();
		read_cont = FinalConts_readXML();
	}

	//cout<< "in.cont=="<<vvP.size()<<endl;

   //read_cont= FinalConts_readXML();
  //cout<< "read_cont=="<<read_cont.size()<<endl;


  // for( int m = 0; m<read_cont.size(); m++ )
  //{
  //drawContours( drawing1, read_cont, m, Scalar(154,255,0), 2, 8);
  // }

   //..........................................................................................................................
	sheetStartX = bounding_rect.x;
	//  cout<< " initial x cordinate::::"<<bounding_rect.x <<endl;
	sheetStartWidthProp = bounding_rect.x / (combinedImg.cols / 100); //in percent for display in UI
				   //cout<< "last x cordinate===" <<bounding_rect.x+bounding_rect.width << endl;
	sheetWidthProp = bounding_rect.width / 7;


	//cout<<"percentage:================================================================== "<<percentage<<endl;;

	percentageRead = read_percentage();
	//cout<<"percentage:=======================================================================]]]]]]]]]]]]]]]]]]]]]] "<<percentageRead<<endl;


	vector<int> CheckCont(vvP.size());
	for (int i = 0; i < CheckCont.size(); i++)
	{
		CheckCont[i] = 0;
	}


	for (int k = 0; k < vvP.size(); k++)
	{
		if (k <= (vvpBfrcont2 - 1))
		{

			double contAr1 = contourArea(vvP[k]);
			//cout<<"con area find:"<<k<<": "<<contAr1<<endl;
			RotatedRect r1 = minAreaRect(vvP[k]);
			int heightf = r1.size.height;
			int widthf = r1.size.width;
			int widthfind = min(heightf, widthf);
			int heightfind = max(heightf, widthf);
			//cout<< "widthf:" << widthfind << endl;
		   //cout<< "heightf:" << heightfind <<endl;



			for (int l = 0; l < read_cont.size(); l++)
			{
				double contAr2 = contourArea(read_cont[l]);
				// cout<<"con area read:"<<l<<": "<<contAr2<<endl;
				RotatedRect r2 = minAreaRect(read_cont[l]);
				int heightR = r2.size.height;
				int widthR = r2.size.width;
				int widthread = min(heightR, widthR);
				int heightread = max(heightR, widthR);
				// cout<< "widthR:" << widthread << endl;
				// cout<< "heightR:" << heightread <<endl;


				// cout<< "area diff=========="<<abs( contAr1- contAr2)<<endl;

				// cout<< "diiference in height================"<<abs(heightread-heightfind)<<endl;

				// cout<< "diiference in width================================"<<abs(widthread-widthfind)<<endl;



				 //if((abs( contAr1- contAr2)<3000) && (abs(heightread-heightfind)<50) && (abs(widthread-widthfind)<50)   )  
				  // if((abs( contAr1- contAr2)<7000) && (abs(heightread-heightfind)<30) && (abs(widthread-widthfind)<30)   )  
				if ((abs(contAr1 - contAr2) < 12000) && (abs(heightread - heightfind) < 600) && (abs(widthread - widthfind) < 50))
				{
					//   cout <<"ok"<<endl;
					CheckCont[k] = 1;
				}
			}
		}
	}
	//
	  //Mat img(inImage.size(), CV_8UC3);

	 //imshow("new image", img);
	RotatedRect RR;

	for (int p = 0; p < CheckCont.size(); p++)
	{
		if (CheckCont[p] == 1)
		{
			if (p > (vvpBfrcont2 - 1))
				drawContours(img(sheetRect), vvP, p, Scalar(0, 255, 0), 2, 8);
			else
				drawContours(img, vvP, p, Scalar(0, 255, 0), 2, 8);

			counter++;

		}
		else
		{
			int position;
			RR = minAreaRect(vvP[p]);
			if (p > (vvpBfrcont2 - 1))
			{
				drawContours(img(sheetRect), vvP, p, Scalar(0, 0, 255), 2, 8);
				position = (int)(((int)RR.center.x) / sheetWidthProp);
			}
			else
			{
				drawContours(img, vvP, p, Scalar(0, 0, 255), 2, 8);
				position = (int)(((int)RR.center.x - sheetStartX) / sheetWidthProp);
			}
			// cout<<"position::"<<position<<endl;
			if (position < 7)
			{ //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());

				outArr[position] += 1;
			}

			//-----------------


			//-----------------
		}

	}
	//	cout<< "no. of ok:" << counter << endl;
	cout << "vvp size::" << vvP.size() << "   counter::" << counter << endl;

	if (vvP.size() == counter)// && (percentage>27 && percentage<33) )
	{
		putText(img, "OK", cv::Point(500, 300), FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 255, 0), 2);
	}
	else
	{
		putText(img, "NOT OK", cv::Point(500, 300), FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0, 255), 2);
		if (vvP.size() != counter)
		{
			putText(img, "contour error", cv::Point(500, 600), FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0, 255), 2);
		}
		/*if(percentage>33 || percentage<27)
		{
		putText(img, "percent error", cv::Point(2000, 800), CV_FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0,255), 2);
		}*/
	}

	/// Show in a window
   //  namedWindow( "Contours", CV_WINDOW_AUTOSIZE );
   //  imshow( "Contours", drawing );
   //
   //  resize(drawing,res,Size(),0.3,0.3,INTER_LINEAR);
   //imshow("resize img45",res);
   //
   //resize(drawing1,res,Size(),0.3,0.3,INTER_LINEAR);
   //imshow("read img",res);
   //
   //resize(img,res,Size(),0.3,0.3,INTER_LINEAR);
   //imshow("fetch contour",res);


	   //---------------------------------------------------------------############################################################
	   //cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
	Mat m1, m2;

	//if(combinedImg.channels()<3)
	//	cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);

	line(img, Point(sheetStartX + sheetWidthProp, 0), Point(sheetStartX + sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 2 * sheetWidthProp, 0), Point(sheetStartX + 2 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 3 * sheetWidthProp, 0), Point(sheetStartX + 3 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 4 * sheetWidthProp, 0), Point(sheetStartX + 4 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 5 * sheetWidthProp, 0), Point(sheetStartX + 5 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 6 * sheetWidthProp, 0), Point(sheetStartX + 6 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	//line(combinedImgColor,Point(sheetWidthProp,0),Point(sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),2);

//------------------------find cont
	//  vector<vector<Point> > contours;
 //   vector<Vec4i> hierarchy;
	//RotatedRect RR;
 // findContours(combinedImg, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
 // for (int i = 0; i < contours.size(); i++) 
 // {  
	//  RR= minAreaRect(contours[i]);
	//  double contArea=contourArea(contours[i]);
	//  if (contArea>=8500  )
	//  drawContours(combinedImgColor, contours, i, Scalar(0, 255, 0), 3, 8, hierarchy, 0, Point());
	// if(contArea<800 &&  contArea>300)
	//  {int position=(int)((int)RR.center.x/sheetWidthProp);
	//	  cout<<"position::"<<position<<endl;
	//  if (position<7)
	//  { //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
	//
	//	 outArr[position]+=1;
	//  }
	//  
	//  }
 // }
 // 
//------------------------------
	out1Prop = outArr[0];
	out2Prop = outArr[1];
	out3Prop = outArr[2];
	out4Prop = outArr[3];
	out5Prop = outArr[4];
	out6Prop = outArr[5];
	out7Prop = outArr[6];




	//-----------------------------


  //	out=1;

	end = clock();
	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
	cout << "Proc Time  in sec " << time_spent << endl;
	return bmp1;

}
//System::Drawing::Bitmap^ processWeb::Class1::processAllFront(System::Drawing::Bitmap^  bmp1,System::Drawing::Bitmap^  bmp2,System::Drawing::Bitmap^  bmp3)
//{clock_t begin, end;
//counter=0;
//	double time_spent;
//	begin = clock();
//	
//	 for (int k=0 ; k<7;k++)
//	 {
//	 outArr[k]=0;
//	 }
//
//	imgC1=BitmapToMat(bmp1);
//	imgC2=BitmapToMat(bmp2);
//	imgC3=BitmapToMat(bmp3);
//	
//	combinedImg=Mat::zeros(imgC1.rows,(imgC1.cols)*2-(160*3)+imgC1.cols,imgC1.type());
//
//	imgC1.copyTo(combinedImg(Rect(0,0,imgC1.cols,imgC1.rows)));
//	imgC2.copyTo(combinedImg(Rect(imgC1.cols-172,0,imgC1.cols,imgC1.rows)));
//	imgC3.copyTo(combinedImg(Rect((imgC1.cols)*2-(172*3),0,imgC1.cols,imgC1.rows)));
//	//----------------save images
//	//imwrite("D:/imgs/c1/1.bmp",imgC1);
//	//imwrite("D:/imgs/c2/1.bmp",imgC2);
//	//imwrite("D:/imgs/c3/1.bmp",imgC3);
//	imwrite("D:/imgs/comb/1.bmp",combinedImg);
//	//---------------------------
//	Mat combinedImgColor;
//	if(combinedImg.channels()<3)
//		cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
//	else
//		combinedImgColor=combinedImg.clone();
//	//------------------------------------------------------------------------------
//	/*threshold(combinedImg,combinedImg,thresholdV ,255,CV_THRESH_BINARY_INV);
//	if (combinedImg.channels()>2)
//		cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);*/
//
//	//---------------------------------------------------------------###########################################################
//	 inImage=combinedImg.clone();//imread("C:/Users/Pradeep Kumar/Desktop/New folder (3)/sand paper/c1/1.bmp");
//
//
////cout<<"inImg channels::"<<inImage.channels()<<endl;
//
//if (inImage.channels()>2)
//	cvtColor(inImage,imgGrey,CV_BGR2GRAY);
//else
//	imgGrey=inImage.clone();
//
////imshow("inputimg",inImage);
//threshold(imgGrey,inImgTh,thresholdVProp ,255,CV_THRESH_BINARY);
//
////imshow("thresimg",inImgTh);
//
//Mat res;
//
////resize(inImage,res,Size(),0.3,0.3);
////imshow("input img",res);
//
////Mat res1;
////resize(inImgTh,res1,Size(),0.3,0.3);
////imshow("thres img",res1);
//
// Mat canny_output;
//  vector<vector<Point> > contours;
//  //vector<vector<int> > vvP.size();
//  
//  vector<vector<Point> > vvP;
//   vector<vector<Point> > read_cont;
//  vector<Vec4i> hierarchy;
//  /// Find contours
//  findContours( inImgTh, contours, hierarchy,CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0) );
//
//  /// Draw contours
//  Mat drawing = Mat::zeros( inImgTh.size(), CV_8UC3 );
//   Mat drawing1 = Mat::zeros( inImgTh.size(), CV_8UC3 );
//
//
//
//
//   //............................................percentage............................................................ 
//  int TotalNumberOfPixels = imgGrey.rows * imgGrey.cols;
// int ZeroPixels = TotalNumberOfPixels - countNonZero(inImgTh);
// //cout<<"The number of pixels black:::::: "<<ZeroPixels<<endl;
////cout<< "total no. of pixels:: "<<TotalNumberOfPixels<<endl;
////cout<< "total no. of pixels white::::::: "<<countNonZero(inImgTh)<<endl;
//   
// //  percentfthresh_callback( );
//percentage = ((float)ZeroPixels/(float)TotalNumberOfPixels)*100;
//
//		// cout<<"percentage of black:::: "<<percentage<<endl;
//
//
// 
////......................................................................................
//Mat img=combinedImgColor.clone();
//int largest_area=0;
// int largest_contour_index=0;
// Rect bounding_rect;
//
//
//
//
//  for( int i = 0; i< contours.size(); i++ )
//  {
//	  double contAr= contourArea(contours[i], true);
//	  RotatedRect r1=minAreaRect(contours[i]) ;
//  int d1=r1.size.height;
//  int d2=r1.size.width;
//  int height=max(d1,d2);
//  int width=min(d1,d2);
//	
//  
//	 double a=contourArea( contours[i],false); 
//       if(a>largest_area)
//	   {
//       largest_area=a;
//       largest_contour_index=i;                
//       bounding_rect=boundingRect(contours[i]);
//
//	   }
//	   //if(bounding_rect.width>22)
//	   //{
//		  // bounding_rect=Rect(bounding_rect.x+20,bounding_rect.y,bounding_rect.width-20,bounding_rect.height);
//	   //}
////if(contAr>14000  )
////     {
//// //cout<<"con area:"<<i<<": "<<contAr<<endl;
////  /*cout<< "width:" << width << endl;
////  cout<< "height:" << height <<endl; */
//// 
////
////		//if(height>210 && height<350)  
////	
////		 int numOfPixels = contours[i].size();
////			   bool edgecont=false;
////		 for (int j = 0; j < contours[i].size() ; j++ )
////			{	   Point coord =  contours[i][j];
////  //  cout  << "coordinates: "<<i  <<"cont Point :"<<j<<" x = " << coord.x<< " y = "<<coord.y << endl;
////				
////				if (coord.y<5 || coord.y>(canny_output.rows-5) ||  coord.x<5  || coord.x>(canny_output.cols-5) )
////				{
////				edgecont=true;
////				}
////				
////				
////				}
////			   if (edgecont==true)
////			   {
////			   		   drawContours(drawing, contours, i, Scalar (0,0,255), 3 ,8,  hierarchy, 0, Point() );
////			   
////			  
////	     vvP.push_back(contours[i]);
////			   }
////     }
//  
//  }
//
//  // drawContours( drawing, contours,largest_contour_index, Scalar(0,0,255), CV_FILLED, 8, hierarchy );
//   rectangle(img, bounding_rect,  Scalar(0,0,255),8, 8,0);  
//   sheetStartX=bounding_rect.x ;
//		   	      cout<< " initial x cordinate::::"<<bounding_rect.x <<endl;
//
//				  cout<< "last x cordinate===" <<bounding_rect.x+bounding_rect.width << endl;
//				  sheetWidthProp=bounding_rect.width/7;
// //...........................................................read & write.................................................... 
//  if (trainTESTProp==true)
// {
// write_percentage( percentage);
// if (combinedImg.channels()>2)
//	 cvtColor(combinedImg,readTempl ,CV_BGR2GRAY );
// readTempl=combinedImg.clone();
// 	imwrite("D:/imgs/comb/1.bmp",combinedImg);
////cout<< "in.cont=="<<percentage.size()<<endl;
//// percentagef=percentage;
// // write_FinalConts(vvP);
//	readFlag=1;
//	
//  }
//  //cout<< "in.cont=="<<vvP.size()<<endl;
//  if(readFlag==0)
//  {
//	  readFlag=1;
// readTempl= imread("D:/imgs/comb/1.bmp");
// if (readTempl.channels()>2)
//	 cvtColor(readTempl,readTempl,CV_BGR2GRAY);
//  }
//// read_cont= FinalConts_readXML();
////cout<< "read_cont=="<<read_cont.size()<<endl;
//
//
//// for( int m = 0; m<read_cont.size(); m++ )
////{
////drawContours( drawing1, read_cont, m, Scalar(154,255,0), 2, 8);
//// }
////
// //..........................................................................................................................
//
//
// cout<<"percentage:================================================================== "<<percentage<<endl;;
//
// percentageRead=read_percentage( );
//  cout<<"percentage:=======================================================================]]]]]]]]]]]]]]]]]]]]]] "<<percentageRead<<endl;
//
//
//// vector<int> CheckCont(vvP.size());
//// for (int i = 0; i<CheckCont.size(); i++)
////{
////	CheckCont[i] = 0;
////}
//
//
// //for( int k = 0; k<vvP.size(); k++ )
// //{  double contAr1= contourArea(vvP[k]);
// ////cout<<"con area find:"<<k<<": "<<contAr1<<endl;
// //RotatedRect r1=minAreaRect(vvP[k]) ;
// // int heightf=r1.size.height;
// // int widthf=r1.size.width;
// // int widthfind=min(heightf,widthf);
// // int heightfind=max(heightf,widthf);
// //  //cout<< "widthf:" << widthfind << endl;
// // //cout<< "heightf:" << heightfind <<endl;
//	//
// // 
// // 
// // for( int l = 0; l<read_cont.size(); l++ )
//	// {  double contAr2= contourArea(read_cont[l]);
//	//// cout<<"con area read:"<<l<<": "<<contAr2<<endl;
//	// RotatedRect r2=minAreaRect(read_cont[l]);
// // int heightR=r2.size.height;
// // int widthR=r2.size.width;
// // int widthread=min(heightR,widthR);
// // int heightread=max(heightR,widthR);
// //// cout<< "widthR:" << widthread << endl;
// //// cout<< "heightR:" << heightread <<endl;
//
// //
// //// cout<< "area diff=========="<<abs( contAr1- contAr2)<<endl;
//
// //// cout<< "diiference in height================"<<abs(heightread-heightfind)<<endl;
//
// //// cout<< "diiference in width================================"<<abs(widthread-widthfind)<<endl;
//
// //  
// // 
// // //if((abs( contAr1- contAr2)<3000) && (abs(heightread-heightfind)<50) && (abs(widthread-widthfind)<50)   )  
// //   if((abs( contAr1- contAr2)<7000) && (abs(heightread-heightfind)<30) && (abs(widthread-widthfind)<30)   )  
//
// //  {
//	////   cout <<"ok"<<endl;
//	//   CheckCont[k] = 1;
// //  }
// //}
// //}
////
//  //Mat img(inImage.size(), CV_8UC3);
// 
// //imshow("new image", img);
//  			RotatedRect RR;
//			int defCount=0;
//			Mat m1,m2;
	//		for (int row=0;row<(readTempl(bounding_rect).rows-49);row=row+50)
	//	{
	//		for(int col=150;col<(readTempl(bounding_rect).cols-150);col=col+50)
	//		{Rect window=Rect(col+bounding_rect.x,row+bounding_rect.y,50,50);
	//		m2=imgGrey(window);
	//		m1=readTempl(window);
	//		 Scalar m = mean(m1);
	//		 float myMAtMean = m.val[0];
	//		  Scalar r = mean(m2);
	//		 float myMAtMean_roi = r.val[0];
	//	// cout<<"myMAtMean_roi"<<m <<endl;
	//	 //cout<<"myMAtMean"<<r<<endl;   
	//	
	//		//Scalar mssim=getMSSIM(m1,m2);
	//		//cout<<"mssim::"<<mssim[0]<<endl;
	//	//	if (mssim[0]<0.1)											
	//			if  (myMAtMean_roi >=(myMAtMean - myMAtMean*0.22) && myMAtMean_roi <=(myMAtMean +myMAtMean*0.22))	//0.22
	//			{ //cout<<"myMAtMean_roi cond=="<<myMAtMean_roi <<endl;
	//				//outCodeProp=1;
	//			}
	//			else
	//			{rectangle(img,Rect(window.x,window.y,window.width,window.height),Scalar(0,0,255),2);
	//				defCount +=1;
	//				int position=(int)((window.x-bounding_rect.x)/sheetWidthProp);
	////	  cout<<"position::"<<position<<endl;
	//				  if (position<7)
	//				  { //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
	//
	//					 outArr[position]+=1;
	//				  }
	//			}

	//		}
	//	}
//// for (int p = 0; p<CheckCont.size(); p++)
////{
////	if(CheckCont[p] == 1)
////	{
////		drawContours( img, vvP, p, Scalar(0,255,0), 2, 8);
////			counter++;
////
////	}
////	else
////	{
////		  RR= minAreaRect(vvP[p]);
////	drawContours( img, vvP, p, Scalar(0,0,255), 2, 8);
////
////	int position=(int)(((int)RR.center.x-sheetStartX)/sheetWidthProp);
////		//  cout<<"position::"<<position<<endl;
////	  if (position<7)
////	  { //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
////	
////		 outArr[position]+=1;
////	  }
////
////	  //-----------------
////
////	
////	  //-----------------
////	}
////	
////}
// //	cout<< "no. of ok:" << counter << endl;
// //cout<<"vvp size::"<<vvP.size()<<"   counter::"<<counter<<endl;
// 
// if(defCount<2)
//{
// putText(img, "OK", cv::Point(500, 300), CV_FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 255, 0), 2);
//}
//else
//{
//	putText(img, "NOT OK", cv::Point(500, 300), CV_FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0,255), 2);
//	/*if(vvP.size()!=counter)
//	{
//		putText(img, "contour error", cv::Point(500, 600), CV_FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0,255), 2);
//	}
//	if(percentage>33 || percentage<27)
//	{
//	putText(img, "percent error", cv::Point(2000, 800), CV_FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0,255), 2);
//	}*/
//}
// 
// /// Show in a window
////  namedWindow( "Contours", CV_WINDOW_AUTOSIZE );
////  imshow( "Contours", drawing );
////
////  resize(drawing,res,Size(),0.3,0.3,INTER_LINEAR);
////imshow("resize img45",res);
////
////resize(drawing1,res,Size(),0.3,0.3,INTER_LINEAR);
////imshow("read img",res);
////
////resize(img,res,Size(),0.3,0.3,INTER_LINEAR);
////imshow("fetch contour",res);
//
//
//	//---------------------------------------------------------------############################################################
//	//cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
////	Mat m1,m2;
//	
//	//if(combinedImg.channels()<3)
//	//	cvtColor(combinedImg,combinedImgColor,CV_GRAY2BGR);
//
//	line(img,Point(sheetStartX  +sheetWidthProp,0),Point(sheetStartX+sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	line(img,Point(sheetStartX+2*sheetWidthProp,0),Point(sheetStartX+2*sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	line(img,Point(sheetStartX+3*sheetWidthProp,0),Point(sheetStartX+3*sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	line(img,Point(sheetStartX+4*sheetWidthProp,0),Point(sheetStartX+4*sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	line(img,Point(sheetStartX+5*sheetWidthProp,0),Point(sheetStartX+5*sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	line(img,Point(sheetStartX+6*sheetWidthProp,0),Point(sheetStartX+6*sheetWidthProp,img.rows-1),Scalar(255,0,0),4);
//	//line(combinedImgColor,Point(sheetWidthProp,0),Point(sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),2);
//
////------------------------find cont
//	//  vector<vector<Point> > contours;
// //   vector<Vec4i> hierarchy;
//	//RotatedRect RR;
// // findContours(combinedImg, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
// // for (int i = 0; i < contours.size(); i++) 
// // {  
//	//  RR= minAreaRect(contours[i]);
//	//  double contArea=contourArea(contours[i]);
//	//  if (contArea>=8500  )
//	//  drawContours(combinedImgColor, contours, i, Scalar(0, 255, 0), 3, 8, hierarchy, 0, Point());
//	// if(contArea<800 &&  contArea>300)
//	//  {int position=(int)((int)RR.center.x/sheetWidthProp);
//	//	  cout<<"position::"<<position<<endl;
//	//  if (position<7)
//	//  { //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
//	//
//	//	 outArr[position]+=1;
//	//  }
//	//  
//	//  }
// // }
// // 
////------------------------------
//		out1Prop=outArr[0];
//		out2Prop=outArr[1];
//		out3Prop=outArr[2];
//	    out4Prop=outArr[3];
//		out5Prop=outArr[4];
//		out6Prop=outArr[5];
//		out7Prop=outArr[6];
//
//
//
//
//  //-----------------------------
//	
//
//	System::Drawing::Bitmap^ returnImg=MatToBitmap(img );
////	out=1;
//
//		end = clock();
//	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
//	cout<<"Proc Time  in sec "<< time_spent <<endl;
//	return returnImg;
//
//} 

float distPoint(Point a, Point b) {
	float dist = sqrt(pow(b.x - a.x, 2) + pow(b.y - a.y, 2));
	return dist;
}
void resShow(string name, Mat img, float scale = 0.2) {
	Mat res;
	resize(img, res, Size(), scale, scale);
	imshow(name, res);
	waitKey(5);
}
void Morph(const cv::Mat& src, cv::Mat& dst, int operation, int kernel_type, int size, int iterCnt = 1)
{
	cv::Point anchor = cv::Point(size, size);
	cv::Mat element = getStructuringElement(kernel_type, cv::Size(2 * size + 1, 2 * size + 1), anchor);
	morphologyEx(src, dst, operation, element, anchor, iterCnt);
}
int get_avgInt(Mat image, Rect roi)
{
	Scalar m = mean(image(roi));
	return m[0];
}
int get_defectCategory(Mat imageIn, RotatedRect rr, int avg_fabricColor)
{
	Rect br = rr.boundingRect();
	Size brSz = br.size();
	Size rrSz = rr.size;
	int rrht = max(rr.size.height, rr.size.width);
	int rrwd = min(rr.size.height, rr.size.width);
	int orientation = (int)abs((rr.angle)) % 90;
	if (brSz.width > 10 && brSz.width < 40 && (((float)brSz.width / (float)brSz.height) < 0.3))
		return 0;//line marks // straight thin rects
	else if (rrwd > 5 && (rrht > 3 * rrwd) && (orientation > 20 && orientation < 70))
		return 1; // wrinkles // rotated thin rects
	else if (rrht > imageIn.cols * 0.75 && rrwd > 100)
		return 3;
	else
	{
		//cout << "br before " << br << endl;
		if (br.x < 0)  //correct roi to eleminate exceptions
			br.x = 0;
		if (br.y < 0)
			br.y = 0;
		if (br.x + br.width >= imageIn.cols)
			br.width = imageIn.cols - br.x - 1;
		if (br.y + br.height >= imageIn.rows)
			br.height = imageIn.rows - br.y - 1;
		//cout << "br after " << br <<"  x2:"<< br.x+br.width<< " y2: "<< br.y+br.height << endl;
		int avgCol = get_avgInt(imageIn, br);
		if ((avg_fabricColor - avgCol) > 40)
			return 2; // hole cut // darker area wrt fabric
		else
			return 4; // other defect // 5 nozzle //
	}

}


struct defectData {
	int idx;
	Point center;
	Rect br;
};
bool sortPositionX(defectData f1, defectData f2)
{
	defectData i = f1;
	defectData j = f2;

	return ((i.center.x < j.center.x));
}
int detectNozzleMarks(vector<defectData> vec, vector<Rect>& returnRects)
{
	int defCnt = 0;
	std::sort(vec.begin(), vec.end(), sortPositionX);
	vector<vector<Point>> defectRows;
	vector<Point> row_of_defects;
	row_of_defects.push_back(vec[0].center);
	for (int i = 1; i < vec.size(); i++)
	{
		if (vec[i].br.width > 1.5 * vec[i].br.height)
		{
			if ((abs(vec[i].center.y - vec[i - 1].center.y)) < 10)
			{
				row_of_defects.push_back(vec[i].center);
				cout << "same row" << endl;
			}
			else
			{
				if (row_of_defects.size() > 3)
				{
					defectRows.push_back(row_of_defects);
					row_of_defects.clear();
					row_of_defects.push_back(vec[i].center);
					cout << "row break +++++++++++++ new row" << endl;
				}

			}
		}

	}
	if (defectRows.size() > 0)
	{
		for (int k = 0; k < defectRows.size(); k++)
		{
			Rect br = boundingRect(defectRows[k]);
			returnRects.push_back(br);
			defCnt++;
		}

	}
	return defCnt;
}


double par_minAr = 20;
int par_minSize = 20;
Rect avgIntensityROI;
vector <string> vec_defectCat = { "line Marks", "wrinkle", "hole or cut","Tape", "other" };
Mat processWeb::Class1::processSandSG(Mat imageIn, int& defectCount, double& defectArea)
{

	Mat image = imageIn.clone();
	Mat drawImg;
	cvtColor(image, drawImg, COLOR_GRAY2BGR);
	Mat cannyRes;
	avgIntensityROI = Rect(250, 50, 600, 60);
	//rectangle(drawImg, avgIntensityROI, Scalar(200, 0, 0), 2);
	uchar intensityV = get_avgInt(image, avgIntensityROI);
	putText(drawImg, "avg Intensity = " + to_string(intensityV), Point(250, 50), FONT_HERSHEY_COMPLEX, 1, Scalar(0, 0, 200), 1);

	//cannyRes = getSobel(image,1,1, 3);
	adaptiveThreshold(image, cannyRes, 255, ADAPTIVE_THRESH_MEAN_C, THRESH_BINARY_INV, defBlockSize, defMinSize);//first1 should be odd 51-551 off set 0-255 
	Morph(cannyRes, cannyRes, MORPH_ERODE, MORPH_ELLIPSE, 1, 1);
	Morph(cannyRes, cannyRes, MORPH_DILATE, MORPH_ELLIPSE, 3, 1);
	//----- finalDefects
	Mat contImgIn = cannyRes.clone();
	//resShow("contImgIn", contImgIn);
	vector< vector<Point> > contours;
	vector<Vec4i> hierarchy;
	findContours(contImgIn, contours, hierarchy, RETR_EXTERNAL, CHAIN_APPROX_NONE);
	double defArea{};
	RotatedRect rr;
	double contAr;
	vector<defectData> vec_defects;
	defectPoint pointLoc;
	for (int i = 0; i < (int)contours.size(); i++)
	{
		contAr = contourArea(contours[i]);
		if (contAr > defMinSize)
		{
			rr = minAreaRect(contours[i]);

			//approxPolyDP(Mat(contours[i]), contours_poly[i], 3, true);
			if (rr.size.height > par_minSize || rr.size.width > par_minSize)
			{//drawContours(binary1,contours,i,0,-1);
				//drawContours(defV, contours, i, 255, -1);
				rectangle(drawImg, rr.boundingRect(), Scalar(0, 0, 100), 1, LINE_4);
				drawContours(drawImg, contours, i, Scalar(0, 0, 255), 1);
				++defectCount;
				//putText(drawImg, cv::format("%.3f", ))
				defArea += contAr;

				pointLoc.tl = rr.boundingRect().tl() * 5;
				pointLoc.br = rr.boundingRect().br() * 5;
				int defectCat = get_defectCategory(image, rr, intensityV);
				pointLoc.defectCat = defectCat;
				//cout << "This is off set " << offSet << endl;
				pointLoc.defectArea = contAr * 25;
				returnDefPoints.push_back(pointLoc);
				//putText(drawImg, vec_defectCat[defectCat], Point(rr.boundingRect().x, rr.boundingRect().y + 40), FONT_HERSHEY_COMPLEX, 0.5, Scalar(0, 100, 100), 1);
				vec_defects.push_back({ i,rr.center,rr.boundingRect() });
			}
		}
	}
	if (vec_defects.size() > 5) // logic for nozzme marks
	{
		vector<Rect> vec_nozzMk;
		int defCnt = detectNozzleMarks(vec_defects, vec_nozzMk);
		cout << "total nozzle marks" << defCnt << endl;
		for (int z = 0; z < defCnt; z++)
		{
			rectangle(drawImg, vec_nozzMk[z], Scalar(0, 0, 255), 4);
			pointLoc.tl = vec_nozzMk[z].tl() * 5;
			pointLoc.br = vec_nozzMk[z].br() * 5;
			pointLoc.defectCat = 5;// nozzle marks
			pointLoc.defectArea = vec_nozzMk[z].area() * 25;
			returnDefPoints.push_back(pointLoc);

		}

	}
	defectArea = defArea;
	//------------------
	//Canny(image, cannyRes,100,200, 7);
	//resShow("canny", cannyRes,1);
	//drawImg.setTo(Scalar(0, 0, 200), cannyRes > 100);
	return drawImg;
}
System::Drawing::Bitmap^ processWeb::Class1::processAllFront(System::Drawing::Bitmap^ bmp1)
{
	clock_t begin, end;
	counter = 0;
	double time_spent;
	begin = clock();

	for (int k = 0; k < 7; k++)
	{
		outArr[k] = 0;
	}

	combinedImg = BitmapToMat(bmp1);


	int startPosC2 = imgC1.cols - overlapC1C2Prop;
	int startPosC3 = (imgC1.cols) * 2 - (overlapC1C2Prop * 2) - (overlapC2C3Prop);
	imgC1.copyTo(combinedImg(Rect(0, 0, imgC1.cols, imgC1.rows)));
	imgC2.copyTo(combinedImg(Rect(startPosC2, 0, imgC1.cols, imgC1.rows)));
	imgC3.copyTo(combinedImg(Rect(startPosC3, 0, imgC1.cols, imgC1.rows)));
	//----------------save images
	//imwrite("D:/imgs/c1/1.bmp",imgC1);
	//imwrite("D:/imgs/c2/1.bmp",imgC2);
	//imwrite("D:/imgs/c3/1.bmp",imgC3);

		//string  path ="D:/imgs/comb/" +to_string ( frameCount) +".bmp";
	//imwrite(path,combinedImg);
	//---------------------------
	frameCount += 1;
	Mat img;
	if (combinedImg.channels() < 3)
		cvtColor(combinedImg, img, COLOR_GRAY2BGR);
	else
		img = combinedImg.clone();
	//------------------------------------------------------------------------------
	/*threshold(combinedImg,combinedImg,thresholdV ,255,CV_THRESH_BINARY_INV);
	if (combinedImg.channels()>2)
		cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);*/

		//---------------------------------------------------------------###########################################################
	inImage = combinedImg.clone();//imread("C:/Users/Pradeep Kumar/Desktop/New folder (3)/sand paper/c1/1.bmp");


//cout<<"inImg channels::"<<inImage.channels()<<endl;

	if (inImage.channels() > 2)
		cvtColor(inImage, imgGrey, COLOR_BGR2GRAY);
	else
		imgGrey = inImage.clone();

	//imshow("inputimg",inImage);
	threshold(imgGrey, inImgTh, th1Prop, 255, THRESH_BINARY);//thresholdVProp//10

	//imshow("thresimg",inImgTh);

	Mat res;

	//resize(inImage,res,Size(),0.3,0.3);
	//imshow("input img",res);

	//Mat res1;
	//resize(inImgTh,res1,Size(),0.3,0.3);
	//imshow("thres img",res1);

	Mat canny_output;
	vector<vector<Point> > contours;
	//vector<vector<int> > vvP.size();

	//vector<vector<Point> > vvP;
	// vector<vector<Point> > read_cont;
	vector<Vec4i> hierarchy;
	/// Find contours
	findContours(inImgTh, contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE, Point(0, 0));

	/// Draw contours
  //.......................................find largest contour for paper boundaries...............................................

	int largest_area = 0;
	int largest_contour_index = 0;
	Rect bounding_rect;




	for (int i = 0; i < contours.size(); i++)
	{
		/*  double contAr= contourArea(contours[i], true);
		  RotatedRect r1=minAreaRect(contours[i]) ;
	  int d1=r1.size.height;
	  int d2=r1.size.width;
	  int height=max(d1,d2);
	  int width=min(d1,d2);*/


		double a = contourArea(contours[i], false);
		if (a > largest_area)
		{
			largest_area = a;
			largest_contour_index = i;
			bounding_rect = boundingRect(contours[i]);

		}


	}

	// drawContours( drawing, contours,largest_contour_index, Scalar(0,0,255), CV_FILLED, 8, hierarchy );
	rectangle(img, bounding_rect, Scalar(0, 0, 255), 8, 8, 0);
	jumboWidthProp = bounding_rect.width * mmperPixProp;
	cout << "This is mmppx " << mmperPix << end;
	sheetStartX = bounding_rect.x;
	//  cout<< " initial x cordinate::::"<<bounding_rect.x <<endl;
	sheetStartWidthProp = bounding_rect.x / (combinedImg.cols / 100);
	//cout<< "last x cordinate===" <<bounding_rect.x+bounding_rect.width << endl;
	//sheetWidthProp=bounding_rect.width/7;

//----------------sobel------------------------------------------------

	int defCount = 0;

	Mat src_gray = imgGrey.clone();
	Mat grad;
	Mat grad_x, grad_y;
	Mat abs_grad_x, abs_grad_y;
	int ksize = 1;
	double scale = 1;
	double delta = 0;

	trimWidth = edgeTrimProp * pixPermmProp;
	if (bounding_rect.width > 2 * trimWidth && ((bounding_rect.width + 2 * trimWidth) < inImgTh.cols))
	{
		bounding_rect = Rect(bounding_rect.x + trimWidth, bounding_rect.y, bounding_rect.width - (2 * trimWidth), bounding_rect.height); //change width of bounding rect to leave edges
		cout << "con1::" << trimWidth << endl;
	}
	else
	{
		bounding_rect = Rect(bounding_rect.x, bounding_rect.y, bounding_rect.width, bounding_rect.height); //change width of bounding rect to leave edges
		cout << "con222::" << trimWidth << endl;
	}


	//bounding_rect =Rect(bounding_rect.x+50,bounding_rect.y,bounding_rect.width-100,bounding_rect.height); //change width of bounding rect to leave edges

	GaussianBlur(src_gray(bounding_rect), src_gray(bounding_rect), Size(3, 3), 0, 0);
	Sobel(src_gray(bounding_rect), grad_x, CV_16S, 1, 0, ksize, scale, delta, BORDER_DEFAULT);
	Sobel(src_gray(bounding_rect), grad_y, CV_16S, 0, 1, ksize, scale, delta, BORDER_DEFAULT);
	// converting back to CV_8U
	convertScaleAbs(grad_x, abs_grad_x);
	convertScaleAbs(grad_y, abs_grad_y);
	addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, grad);
	//grad=grad;
	threshold(grad, grad, th3Prop, 255, THRESH_BINARY);//12//thresholdProp
	contours.clear();
	hierarchy.clear();
	Rect centerCont;
	//	
	//	adaptiveThreshold(src_gray(bounding_rect),grad,255,CV_ADAPTIVE_THRESH_GAUSSIAN_C,CV_THRESH_BINARY,91,0);
	//	cvtColor(grad,img,CV_GRAY2BGR);
		//------------------------------------
	int blockSize = th2Prop;//16
	float percentNG = (0.20 * blockSize * blockSize);
	Rect window, wl, wr, wu, wd;
	//Mat m1,ml,mr,mu,md;
	float myMAtMean;
	float al, ar, au, ad;
	for (int row = blockSize; row < (grad.rows - 2 * blockSize); row = row + blockSize)
	{
		for (int col = blockSize; col < (grad.cols - 2 * blockSize); col = col + blockSize)
		{
			window = Rect(col, row, blockSize, blockSize);
			wl = Rect(col - blockSize, row, blockSize, blockSize);
			wr = Rect(col + blockSize, row, blockSize, blockSize);
			wu = Rect(col, row - blockSize, blockSize, blockSize);
			wd = Rect(col, row + blockSize, blockSize, blockSize);
			//m2=imgGrey(window);
	/*		m1=grad(window);
			ml=grad(wl);
			mr=grad(wr);
			mu=grad(wu);
			md=grad(wd);*/

			//Scalar m = mean(m1);
			myMAtMean = countNonZero(grad(window));
			al = countNonZero(grad(wl));
			ar = countNonZero(grad(wr));
			au = countNonZero(grad(wu));
			ad = countNonZero(grad(wd));
			/*Scalar r = mean(m2);
		 float myMAtMean_roi = r.val[0];*/
		 // cout<<"myMAtMean_roi"<<m <<endl;
		  //cout<<"myMAtMean"<<r<<endl;   

			 //Scalar mssim=getMSSIM(m1,m2);
			 //cout<<"mssim::"<<mssim[0]<<endl;
		 //	if (mssim[0]<0.1)											
			if ((myMAtMean > percentNG) && ((al > percentNG) || (ar > percentNG) || (au > percentNG) || (ad > percentNG)))	//0.22
			{ //cout<<"myMAtMean_roi cond=="<<myMAtMean_roi <<endl;
				rectangle(img, Rect(window.x + bounding_rect.x, window.y + bounding_rect.y, window.width, window.height), Scalar(0, 0, 255), -1);
				//rectangle(img,Rect(window.x,window.y,window.width,window.height),Scalar(0,0,255),2);
				defCount += 1;
				int position = (int)((window.x) / sheetWidthProp);//-bounding_rect.x
//	  cout<<"position::"<<position<<endl;
				if (position < 7)
				{ //drawContours(combinedImgColor, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());

					outArr[position] += 1;
				}
				//outCodeProp=1;
			}
			else
			{
			}

		}
	}
	//		//-----------------------------------------------------------------


	// findContours( grad, contours, hierarchy,CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(bounding_rect.x,bounding_rect.y) );
 //  for( int i = 0; i< contours.size(); i++ )
 // {if (contourArea(contours[i])>80)
 //  {
 // //drawContours(img, contours, i, Scalar (0,0,255), 3 ,8,  hierarchy, 0, Point() );
 //  centerCont=boundingRect(contours[i]);
 //  int position=(int)(((centerCont.x-centerCont.width/2)-bounding_rect.x)/sheetWidthProp);
	////	  cout<<"position::"<<position<<endl;
	//				  if (position<7)
	//				  { drawContours(img, contours, i, Scalar(0,  0,255), -1, 8, hierarchy, 0, Point());
	//
	//					 outArr[position]+=1;
	//				  }
	//				  defCount+=1;
 //  }
 //  }

 //...........................................................read & write.................................................... 




	if (defCount < 2)
	{
		putText(img, "OK", cv::Point(500, 300), FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 255, 0), 2);
	}
	else
	{
		putText(img, "NOT OK", cv::Point(500, 300), FONT_HERSHEY_SIMPLEX, 3, Scalar(0, 0, 255), 2);

	}



	line(img, Point(sheetStartX + sheetWidthProp, 0), Point(sheetStartX + sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 2 * sheetWidthProp, 0), Point(sheetStartX + 2 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 3 * sheetWidthProp, 0), Point(sheetStartX + 3 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 4 * sheetWidthProp, 0), Point(sheetStartX + 4 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 5 * sheetWidthProp, 0), Point(sheetStartX + 5 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	line(img, Point(sheetStartX + 6 * sheetWidthProp, 0), Point(sheetStartX + 6 * sheetWidthProp, img.rows - 1), Scalar(255, 0, 0), 4);
	//line(combinedImgColor,Point(sheetWidthProp,0),Point(sheetWidthProp,combinedImgColor.rows-1),Scalar(255,0,0),2);

	out1Prop = outArr[0];
	out2Prop = outArr[1];
	out3Prop = outArr[2];
	out4Prop = outArr[3];
	out5Prop = outArr[4];
	out6Prop = outArr[5];
	out7Prop = outArr[6];




	//-----------------------------
	resize(img, retImgCS, imgC1.size());


	//System::Drawing::Bitmap^ returnImg=MatToBitmap(img );
//	string  path2 ="D:/imgs/res/" +to_string ( frameCount) +".bmp";
//	imwrite(path2,img);
//	out=1;

	end = clock();
	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
	cout << "Proc Time  in sec " << time_spent << endl;
	return bmp1;

}
Mat black, cloneCont;
Mat getPaperEdges(Mat inImgGr, int* startx, int* endx, float* dist)
{
	//----------resize inImage by2 to get faster results----------------------
	Mat imgIn;
	resize(inImgGr, imgIn, Size(), 0.25, 0.25);
	clock_t begin, end;
	begin = clock();
	Mat src = inImgGr;
	vector<int> graph(src.cols);
	Mat roi;
	for (int c = 0; c < src.cols - 1; c++)
	{
		roi = src(Rect(c, 0, 1, src.rows));
		graph[c] = int(mean(roi)[0]);
	}
	Mat mgraph = Mat::zeros(260, src.cols + 10, CV_8UC3);
	/*for (int c=0; c<src.cols-1; c++)
	{
		line( mgraph, Point(c+5,259-0), Point(c+5,259-graph[c]), Scalar(255,0,0), 1, CV_AA);
	}*/
	graph[0] = 0;
	graph[graph.size() - 1] = 0;

	int paperStart = 0, paperEnd = 0, paperWidth = 0;
	int pst = 0, pnd = 0;
	bool started = false;
	bool ended = true;
	for (int idx = 100; idx < graph.size() - 5; idx++)
	{
		if ((graph[idx - 1] - graph[idx]) > 10 && graph[idx + 5] < graph[idx] && graph[idx]>50 && ended == true && started == false)
		{
			pst = idx;
			started = true;
			ended = false;
		}
		if ((graph[idx] - graph[idx - 1]) > 5 && graph[idx - 5] < graph[idx] && graph[idx]>50 && started == true && ended == false)
		{
			ended = true;
			started = false;
			pnd = idx;
			if (paperWidth < (pnd - pst))
			{
				paperWidth = (pnd - pst);
				paperStart = pst;
				paperEnd = pnd;

			}

		}

	}
	cout << "Image size 0.4" << src.size() << endl;
	cout << "Paper start " << paperStart << endl;
	cout << "Paper End " << paperEnd << endl;
	*startx = paperStart;
	*endx = paperEnd;
	line(src, Point(paperStart + 5, 259 - 0), Point(paperStart + 5, 0), Scalar(0, 255, 0), 20);
	line(src, Point(paperEnd + 5, 259 - 0), Point(paperEnd + 5, 0), Scalar(0, 0, 255), 20);
	*dist = distPoint(Point(paperStart + 5, 0), Point(paperEnd + 5, 0));

	end = clock();
	cout << "time taken " << (double)(end - begin) / CLOCKS_PER_SEC << endl;
	// resize(inImgGr, inImgGr, Size(), 4, 4);

	return inImgGr;

	//--------------------------------------------------------------------
	   //Mat resz;
	   //resize(inImgGr,resz,Size(),0.1,0.1);
	//imshow("mgraph", resz);
	//resize(src,resz,Size(),0.1,0.1);
	//imshow("source", resz);
	//return inImgGr;
	   ////--------------------------------
	   //Mat img=inImgGr.clone();
	   //Mat imgColor;
	   //cvtColor(img,imgColor,COLOR_GRAY2BGR);
	   //int pixVal=0,pixValPrv=0,pixValPostPrv=0, pixNext=0;
	   //for (int rowNo=20;rowNo<(img.rows-20);rowNo+=4)
	   //{int diff_cnt=0;
	   // int diff =0;
	   // int diffP=0;
	   //int diffN=0;
	   //	for (int colNo=20;colNo<(img.cols-20);colNo++)
	   //	{
	   //		pixNext=img.at<uchar>(rowNo,colNo+1);
	   //	pixVal=img.at<uchar>(rowNo,colNo );
	//		pixValPrv=img.at<uchar>(rowNo,colNo-1 );
	   //	pixValPostPrv=img.at<uchar>(rowNo,colNo-2);

	   //	  if((pixVal==pixNext) && (pixVal!=pixValPrv) && (pixVal!=pixValPostPrv ))
	   //	  {// circle(imgColor,Point(colNo,rowNo ),4,Scalar(0,0,230),-1);
	   //  
	   //	  }

	   //	}
	   //}

	   //resize(imgColor,resz,Size(),0.1,0.1);
	   //imshow("edge",resz);
	   //return imgColor;
}



void  MergeImage(Mat& image_1, Mat& image_2, Mat& added_image) {

	added_image = Mat::zeros(Size(image_1.cols * 2, image_1.rows), image_1.type());
	image_1.copyTo(added_image(Rect(0, 0, image_1.cols, image_1.rows)));
	image_1.copyTo(added_image(Rect(image_1.cols, 0, image_2.cols, image_2.rows)));
}

Mat fineGritDiff;
Rect br_fg;
Rect br_fg_shift;
System::Drawing::Bitmap^ processWeb::Class1::processAllFrontThick(System::Drawing::Bitmap^ bmp1)
{
	clock_t begin, end;
	float dist{};
	counter = 0;
	double time_spent;
	begin = clock();
	//for (int k=0 ; k<7;k++)
	//{
	   //outArr[k]=0;
	//}

	//cout << "Bmp to mat successful" << endl;
	//clock_t start, endtime;
	////	float dist{};
	////	counter = 0;
	////	double time_spent;
	//start = clock();
	//imgC1 = BitmapToMat(bmp1);
	//imgC2 = BitmapToMat(bmp2);
	//Mat _combinedImg = BitmapToMat(bmp3);
	//resize(_combinedImg, _combinedImg, Size(bmp3->Width, bmp3->Height));
	//

	//
	//int st1 = imgC1.cols;
	////int st2 = (imgC1.cols) * 2 - (overlapC1C2Prop * 2) - (overlapC2C3Prop);
	//imgC1.copyTo(_combinedImg(Rect(0, 0, st1, imgC1.rows)));
	//imgC2.copyTo(_combinedImg(Rect(st1, 0, imgC2.cols, imgC2.rows)));

	//endtime = clock();
	//cout << "Time spent in cpp merge " << (double)(endtime - start) / CLOCKS_PER_SEC << endl;
	////Mat finalImage = BitmapToMat(bmp1);
	Mat finalImage = BitmapToMat(bmp1);
	// resShow("finalImage", finalImage);
	Mat resizedImage;
	resize(finalImage, resizedImage, Size(), 0.2, 0.2);
	// resShow("resizedImage", resizedImage);

	Mat grayImage;
	if (resizedImage.channels() > 2)
	{
		cvtColor(resizedImage, grayImage, COLOR_BGR2GRAY);

	}
	else
	{
		grayImage = resizedImage;
	}
	Mat img_1;
	//imshow("1", img_1);
	/*waitKey(0);*/
	img_1 = resizedImage;
	//resShow("img1", img_1);
	/*imshow("1", img_1);
	waitKey(0);*/
	//resShow("img1", img_1);

	Rect brect(0, 0, img_1.cols, 300);
	combinedImg = img_1(brect);

	int startPosC2 = imgC1.cols - overlapC1C2Prop;
	int startPosC3 = (imgC1.cols) * 2 - (overlapC1C2Prop * 2) - (overlapC2C3Prop);

	//----------------save images
	//imwrite("D:/imgs/c1/1.bmp",imgC1);
	//imwrite("D:/imgs/c2/1.bmp",imgC2);
	//imwrite("D:/imgs/c3/1.bmp",imgC3);
	//
	//	string  path ="D:/imgs/comb/" +to_string ( frameCount) +".bmp";
	//imwrite(path,combinedImg);
	//---------------------------
	frameCount += 1;
	Mat img;
	/*if(combinedImg.channels()<3)
		cvtColor(combinedImg,img,CV_GRAY2BGR);
	else*/
	img = resizedImage.clone();
	//resShow("img", img, 0.3);

//------------------------------------------------------------------------------
/*threshold(combinedImg,combinedImg,thresholdV ,255,CV_THRESH_BINARY_INV);
if (combinedImg.channels()>2)
	cvtColor(combinedImg,combinedImg,CV_BGR2GRAY);*/

	//---------------------------------------------------------------###########################################################
	inImage = combinedImg.clone();//imread("C:/Users/Pradeep Kumar/Desktop/New folder (3)/sand paper/c1/1.bmp");
	if (frameCount == 0)
	{
		boundingRectPriv = Rect(0, 0, combinedImg.cols, combinedImg.rows);
	}

	//cout<<"inImg channels::"<<inImage.channels()<<endl;

	if (inImage.channels() > 2)
		cvtColor(inImage, imgGrey, COLOR_BGR2GRAY);
	else
		imgGrey = inImage.clone();


	//imshow("inputimg", imgGrey);

		//GaussianBlur(imgGrey,imgGrey,Size(3,3),0,0);
		//cout << "GaussianBlur" << endl;
		//resShow("image grey", imgGrey);

	threshold(imgGrey, inImgTh, th1Prop, 255, THRESH_BINARY_INV);//thresholdVProp //6
	//resShow("image Thrsh", inImgTh);

	int pprStrt = 0;
	int pprEnd = inImage.cols - 1;
	//-----------------------------------------------------------  
	getPaperEdges(inImgTh, &pprStrt, &pprEnd, &dist);  //get paper edges
	cout << "Dist width " << (dist * 5) * mmperPixProp << endl;
	sheetWidthProp = (dist * 5) * mmperPixProp;


	////.......................................find largest contour for paper boundaries...............................................

	Rect bounding_rect;

	bounding_rect = Rect(pprStrt, 0, pprEnd - pprStrt, inImgTh.rows);
	sheetStartX = bounding_rect.x;
	// //----------------sobel------------------------------------------------
	//   cout<<"sheet Width:"<<sheetWidthProp <<endl;
	int defCount = 0;
	if (bounding_rect.width > sheetWidthProp* sheetsInaRowProp)
	{
		boundingRectPriv = bounding_rect;

		///	line( img , Point(pprStrt, img_1.size[0]/2), Point(pprEnd, img_1.size[0]/2), Scalar(0,255,0), 10, 8);
		out1Prop = pprStrt * 5;
		out2Prop = pprEnd * 5;
		//	resShow("img_1", img);
		//	line( img , Point(pprEnd,0), Point(pprEnd,img.rows), Scalar(0,0,255), 20, LINE_AA);

	}
	else
	{
		bounding_rect = boundingRectPriv;
		//	line(img, Point(pprStrt, img_1.size[0] / 2), Point(pprEnd, img_1.size[0] / 2), Scalar(0, 255, 0), 10, 8);


			//line( img , Point(bounding_rect.x,0), Point(bounding_rect.x,img.rows), Scalar(0,255,0), 20, CV_AA);    
		 //   line( img , Point(bounding_rect.x+bounding_rect.width ,0), Point(bounding_rect.x+bounding_rect.width,img.rows), Scalar(0,0,255), 20, CV_AA);
			//line( img , Point(pprStrt,0), Point(pprStrt,img.rows), Scalar(120,205,0), 20, CV_AA);    
		 //   line( img , Point(pprEnd,0), Point(pprEnd,img.rows), Scalar(120,0,205), 20, CV_AA);
	}
	//resShow("img", img);
	jumboWidthProp = bounding_rect.width * mmperPixProp;
	sheetStartWidthProp = bounding_rect.x / (combinedImg.cols / 100); //in percent for display in UI



	int defectCount = 0;
	double defectArea = 0;
	Mat imageIn;
	Mat croppedImage;
	//resize(grayImage, imageIn, Size(), 0.2, 0.2);
	//cout << "gImage width "<<grayImage.cols << endl;
	imageIn = grayImage.clone();
	//cout << "Line exec" << endl;
	Rect roi = Rect(0.15 * imageIn.cols, 0, imageIn.cols - (0.27 * imageIn.cols), imageIn.rows);

	//cout <<"This is roi X" << roi.tl().x * 5<< endl;

	imageIn = imageIn(roi);

	croppedImage = imageIn.clone();
	//cout << "Cropped Image width " << croppedImage.cols << endl;

	//imshow("Cropped Image", croppedImage);
	//resShow("croppedImage", croppedImage);



	returnDefPoints.clear();
	Mat detections = processSandSG(croppedImage, defectCount, defectArea);
	defectCountProp = defectCount;
	defAreaSizeProp = returnDefPoints.size();
	defectArea *= 25;
	float defectAreamm = defectArea * pow(mmperPixProp, 2);
	defectAreaProp = defectAreamm;
	brect = Rect(Point(roi.tl().x, roi.tl().y), Point(roi.br().x, roi.br().y));
	//resize(detections, detections, brect.size());
	//img(bounding_rect) = detections;
	//cvtColor(img, img, COLOR_GRAY2BGR);



	detections.copyTo(img(brect));

	out1 = brect.tl().x * 5;
	//resize(detections, detections, Size(), 1, 1);
	//img_1(bounding_rect) = detections.clone();
	line(img, Point(pprStrt, img_1.size[0] / 2), Point(pprEnd, img_1.size[0] / 2), Scalar(0, 255, 0), 10, 8);

	//detections.copyTo(img_1(roi));
	//line(img, Point(pprStrt * 4, 50), Point(pprEnd * 4, 50), Scalar(0, 255, 0), 5);
	//resize(img,resizedImage , resizedImage.size());
	resize(img, finalImage, finalImage.size());
	end = clock();
	time_spent = (double)(end - begin) / CLOCKS_PER_SEC;
	cout << "Proc Time  in sec " << time_spent << endl;

	return bmp1;
}


System::Drawing::Bitmap^ processWeb::Class1::mergeImagesCpp(System::Drawing::Bitmap^ bmp1, System::Drawing::Bitmap^ bmp2, System::Drawing::Bitmap^ bmp3)
{
	imgC1 = BitmapToMat(bmp1);
	imgC2 = BitmapToMat(bmp2);
	combinedImg = BitmapToMat(bmp3);
	resize(combinedImg, combinedImg, Size(bmp3->Width, bmp3->Height));

	cout << "BMP1 " << imgC1.cols << " " << imgC1.rows << endl;
	cout << "BMP2 " << imgC2.cols << " " << imgC2.rows << endl;
	cout << "BMP3 " << combinedImg.cols << " " << combinedImg.rows << endl;
	int st1 = imgC1.cols;
	//int st2 = (imgC1.cols) * 2 - (overlapC1C2Prop * 2) - (overlapC2C3Prop);
	imgC1.copyTo(combinedImg(Rect(0, 0, st1, imgC1.rows)));
	imgC2.copyTo(combinedImg(Rect(st1, 0, imgC2.cols, imgC2.rows)));
	cout << "Overlap " << overlapC1C2Prop << endl;
	return bmp3;
}

System::Drawing::Bitmap^ processWeb::Class1::showGuides(System::Drawing::Bitmap^ bitmap0)
{

	Mat region = BitmapToMat(bitmap0);
	try {
		cout << "small run camera 1::::::::::::::" << region.channels() << endl;

		//putText(region, " OK...", Point(100,200), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
		int code = 1;
		//clock_t tStart1 = clock();
		//cout << "time taken111111111111111111111111111111111111111111::::::::" << double(clock() - tStart1) / CLOCKS_PER_SEC << endl;
		//cout << "1922" << endl;
		if (code == 1)
		{
			putText(region, " NG", Point(50, 60), FONT_HERSHEY_DUPLEX, 5, Scalar(0, 0, 255), 5);
		}
		else
		{
			putText(region, " OK", Point(50, 60), FONT_HERSHEY_DUPLEX, 5, Scalar(0, 250, 0), 5);
		}

		return bitmap0;
	}
	catch (exception ex)
	{
		putText(region, " Exception", Point(100, 500), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
		return bitmap0;
		//  System::Drawing::Bitmap^ dst=MatToBitmap(rotate_return)
	}

}

int Class1::output()
{
	try {
		return out;
	} /// END OF TRY BLOCK
	catch (cv::Exception & e) {
		const char* err_msg = e.what();
		std::cout << "exception caught: " << err_msg << std::endl;
	}
}

System::Drawing::Point Class1::getTopLeftPoint(int sectorNum) {
	System::Drawing::Point np;
	np.X = returnDefPoints[sectorNum].tl.x + out1;
	np.Y = returnDefPoints[sectorNum].tl.y;
	return np;
}

System::Drawing::Point Class1::getBottomRightPoint(int sectorNum) {
	System::Drawing::Point np;
	np.X = returnDefPoints[sectorNum].br.x + out1;
	np.Y = returnDefPoints[sectorNum].br.y;
	return np;
}

int Class1::getDefectCat(int sectorNum) {
	return returnDefPoints[sectorNum].defectCat;
}
double Class1::getDefectArea(int sectorNum) {
	return returnDefPoints[sectorNum].defectArea * pow(mmperPixProp, 2);
}