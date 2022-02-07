#include "Stdafx.h"
#include <opencv2/opencv.hpp>
#include <opencv2/core/ocl.hpp>
#include <iostream>
#include "EdgesSubPix.h"
using namespace cv;
using namespace std;
string path_s = "D:/CV/7sept vapi/algo/";

//float distancePoint(cv::Point2f p, cv::Point2f q) {
//	cv::Point2f diff;
//	diff.x=abs( p.x - q.x);
//	diff.y=abs(p.y-q.y);
//	return cv::sqrt(diff.x*diff.x + diff.y*diff.y);
//}
float distancePoint(cv::Point2f p, cv::Point2f q) {
	cv::Point2f diff = p - q;
	return cv::sqrt(diff.x * diff.x + diff.y * diff.y);
}
int main(int argc, char* argv[])
{
	//ocl::setUseOpenCL(false);
	//const String keys =
	//                    "{help h usage ? |          | print this message            }"
	//                    "{@image         |          | image for edge detection      }"
	//                    "{@output        |edge.tiff | image for draw contours       }"
	//                    "{data           |          | edges data in txt format      }"
	//                    "{low            |40        | low threshold                 }"
	//                    "{high           |100       | high threshold                }"
	//                    "{mode           |1         | same as cv::findContours      }"
	//                    "{alpha          |1.0       | gaussian alpha              }";
  //  CommandLineParser parser(argc, argv, keys);
  /*  parser.about("subpixel edge detection");

	if (!parser.check())
	{
		parser.printErrors();
		return 0;
	}

	if (parser.has("help"))
	{
		parser.printMessage();
		return 0;
	}

	if (!parser.has("@image"))
	{
		parser.printMessage();
		return 0;
	}
*/
/*   String imageFile = parser.get<String>(0);
   String outputFile = parser.get<String>("@output");
   int low = parser.get<int>("low");
   int high = parser.get<int>("high");
   double alpha = parser.get<double>("alpha");
   int mode = parser.get<int>("mode");*/
   //String imageFile = "D:/CV/ChandraRing/measure/ok 020 zinc/IMG0050.jpg";
	string imageFile = "D:/CV/ChandraRing/measure/not ok drg 020/IMG0019.jpg";
	//not ok drg 020/IMG0012.jpg
		//String imageFile = "D:/CV/7sept vapi/algo/SHIV-4-MM-NEW -17nov8pp_1mm.bmp";
   //String outputFile = parser.get<String>("@output");
	int low = 40;
	int high = 100;
	double alpha = 1.0;
	int mode = 1;
	Mat image = imread(imageFile);
	if (image.channels() > 2)
	{
		cvtColor(image, image, COLOR_BGR2GRAY);
	}
	//threshold(image,image,120,255,CV_THRESH_TOZERO);
	Mat imageC;
	cvtColor(image, imageC, COLOR_GRAY2BGR);
	imshow("inimg", image);
	waitKey();
	vector<Contour> contours;
	vector<Contour> uniqueContours;

	vector<Vec4i> hierarchy;
	int64 t0 = getCPUTickCount();
	EdgesSubPix(image, alpha, low, high, contours, hierarchy, mode);
	int64 t1 = getCPUTickCount();
	cout << "execution time is " << (t1 - t0) / (double)getTickFrequency() << " seconds" << endl;
	cout << "contours size:" << contours.size() << endl;
	/*vector < vector < cv::Point2f > > contours2;
	for (int i=0;i<contours[0].points.size();i++)
	{
	contours2[0].push_back(cv::Point2f(contours[0].points[i].x,contours[0].points[i].y));
	}
	cout<<"cont point size()"<<contours2[0].size()<<endl;
	drawContours(imageC,contours2 ,0 , Scalar(0, 255, 0), 5, 8);
	waitKey();*/
	vector<vector<cv::Point2f> > conts;

	vector<RotatedRect > finalConts;

	vector<RotatedRect > finalContsWoBig;

	Mat rr;
	for (int i = 0; i < contours.size(); i++)
	{
		RotatedRect  brect = minAreaRect(contours[i].points);

		cout << "center:" << i << ":" << brect.center << endl;
		//cout<<"height_"<<i<<"=="<<brect.size.height*0.25<<endl;
			//cout<<"width_"<<i<<"=="<<brect.size.width*0.25<<endl;
		float heightR = max(brect.size.height, brect.size.width);
		float widthR = min(brect.size.height, brect.size.width);
		if (i == 0)
		{
			finalConts.push_back(brect);
			uniqueContours.push_back(contours[i]);

			//conts.push_back(contours[i].points);
			/*circle(imageC,brect.center,5,Scalar(255,255,0),4);

				putText(imageC,cv::format("%3.2f",0.25*max(brect.size.height,brect.size.width)),brect.center,CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));
				putText(imageC,cv::format("%3.2f",0.25*min(brect.size.height,brect.size.width)),Point(brect.center.x,brect.center.y+60),CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));*/
		}
		//resize(imageC,rr,Size(),0.35,0.35);
//	imshow("rr",rr);
//	waitKey();
		bool unique = true;
		for (int j = 0; j < finalConts.size(); j++)
		{
			if ((abs(finalConts[j].center.x - brect.center.x) < 5) && (abs(finalConts[j].center.y - brect.center.y) < 5))
			{
				float height = max(finalConts[j].size.height, finalConts[j].size.width);
				float width = min(finalConts[j].size.height, finalConts[j].size.width);
				float hdiff = abs(height - heightR);
				float wdiff = abs(width - widthR);
				if (hdiff < 8 && wdiff < 8)
				{
					if (height < heightR && width < widthR)
					{
						finalConts.at(j) = brect;

					}
					unique = false;
					break;
				}




			}


		}

		if (unique == true)
		{
			finalConts.push_back(brect);
			uniqueContours.push_back(contours[i]);
		}

	}

	cout << "finalConts size:" << finalConts.size() << endl;
	cout << "UniqueConts size:" << uniqueContours.size() << endl;
	Point2f vertices[4];

	int contValid = 0;
	Contour contInner;
	Contour contOuter;
	Point2f centOuter;
	Point2f centInner;
	float heightMax, widthMax;
	float heightMin, widthMin;

	int entryk = 0;
	for (int k = 0; k < finalConts.size(); k++)//print height width of found unique conts
	{
		int height = max(finalConts[k].size.height, finalConts[k].size.width);
		int width = min(finalConts[k].size.height, finalConts[k].size.width);
		if (width > 50 && height > 50)
		{
			if (entryk == 0)
			{
				heightMax = max(finalConts[k].size.height, finalConts[k].size.width);
				widthMax = min(finalConts[k].size.height, finalConts[k].size.width);
				heightMin = max(finalConts[k].size.height, finalConts[k].size.width);
				widthMin = min(finalConts[k].size.height, finalConts[k].size.width);
				contInner = uniqueContours[k];
				contOuter = uniqueContours[k];
				entryk = 1;
				centInner = finalConts[k].center;
				centOuter = finalConts[k].center;
			}
			float height = max(finalConts[k].size.height, finalConts[k].size.width);
			float width = min(finalConts[k].size.height, finalConts[k].size.width);
			if ((width > widthMax) && (height > heightMax))
			{
				contOuter = uniqueContours[k];
				centOuter = finalConts[k].center;
			}

			if ((width < widthMin) && (height < heightMin))
			{
				contInner = uniqueContours[k];
				centInner = finalConts[k].center;
			}
			if (height > 50 && width > 50)
			{
				contValid += 1;
				//circle(imageC,finalConts[k].center,5,Scalar(255,255,0),4);
				cout << "cont" << k << finalConts[k].center << endl;
				/*   finalConts[k].points(vertices);

			  for (int ii = 0; ii < 4; ii++) {
				  line(imageC, vertices[ii], vertices[(ii+1)%4], Scalar(0,255,0),2);}*/
				  ////	putText(imageC,cv::format("%3.2f",0.25*height),finalConts[k].center,CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));
				  //	putText(imageC,cv::format("%3.2f",0.25*width),Point(finalConts[k].center.x,finalConts[k].center.y+60),CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));
			}

		}



	}

	circle(imageC, centInner, 5, Scalar(255, 0, 180), 1);
	circle(imageC, centOuter, 10, Scalar(0, 210, 180), 1);
	cout << "inner Size" << contInner.points.size() << endl;
	cout << "outer Size" << contOuter.points.size() << endl;



	cout << "inner C" << centInner << endl;
	cout << "outer C" << centOuter << endl;

	vector <Point2f> minDistMatch;//(contInner.points.size());
	const int arrSize = contInner.points.size();
	vector <float> contInnDist;
	vector <float> contOutDist;
	float dist;
	float sum = 0;
	float meanInn;
	float meanOut;

	//check circularity ID  calculate mean distance
	for (int cntIn = 0; cntIn < contInner.points.size(); cntIn++)
	{
		dist = distancePoint(contInner.points[cntIn], centInner);
		//cout<<"distance=="<<dist<<endl;
		//circle(imageC,contInner.points[cntIn],1,Scalar(0,0,180),1);
		contInnDist.push_back(dist);
		sum = sum + dist;
		float cnt = (float)cntIn + 1.0;
		meanInn = sum / cnt;
	}
	cout << "mean ID ==" << meanInn << endl;
	putText(imageC, cv::format("%3.2f", meanInn), centInner, FONT_HERSHEY_COMPLEX, 1, Scalar(0, 125, 180));
	sum = 0;
	//check circularity OD
	for (int cntOut = 0; cntOut < contOuter.points.size(); cntOut++)
	{
		dist = distancePoint(contOuter.points[cntOut], centOuter);
		//cout<<"distance=="<<dist<<endl;
		//circle(imageC,contOuter.points[cntOut],1,Scalar(0,180,0),1);
		contOutDist.push_back(dist);
		sum = sum + dist;
		float cnt = (float)cntOut + 1.0;
		meanOut = sum / cnt;
	}

	cout << "mean OD ==" << meanOut << endl;
	putText(imageC, cv::format("%3.2f", meanOut), centOuter + Point2f(0, 30), FONT_HERSHEY_COMPLEX, 1, Scalar(0, 125, 180));
	float concentricity = distancePoint(centInner, centOuter);
	putText(imageC, cv::format("Concentricity: %3.2f", concentricity), Point2f(50, 50), FONT_HERSHEY_COMPLEX, 1, Scalar(0, 125, 120));
	for (int cntIn = 0; cntIn < contInner.points.size(); cntIn++)
	{
		float diff = abs(meanInn - contInnDist.at(cntIn));

		if (diff < 3.0)
			circle(imageC, contInner.points[cntIn], 1, Scalar(0, 180, 0), 1);
		else
			circle(imageC, contInner.points[cntIn], 1, Scalar(0, 0, 180), 1);

	}

	for (int cntOut = 0; cntOut < contOuter.points.size(); cntOut++)
	{
		float diff = abs(meanOut - contOutDist.at(cntOut));

		if (diff < 3.0)
			circle(imageC, contOuter.points[cntOut], 1, Scalar(0, 180, 0), 1);
		else
			circle(imageC, contOuter.points[cntOut], 1, Scalar(0, 0, 180), 1);

	}
	//for(int cntIn=0; cntIn<contInner.points.size(); cntIn+=40)
	//{  circle(imageC,(Point)contInner.points[cntIn],10,Scalar(180,0,0),4);
	//float minD=0;
	//Point2f minDpt=Point2f(0,0);
	//	for (int cntOut=0;cntOut<contOuter.points.size();cntOut++)
	//	{float dist=distancePoint(contInner.points[cntIn],contOuter.points[cntOut]);
	//		if (cntOut==0)
	//			{minD=dist;
	//			minDpt=contOuter.points[cntOut];
	//			}
	//	if (dist<minD)
	//	{minD=dist;
	//	minDpt=contOuter.points[cntOut];
	//	}
	//	//circle(imageC,contOuter.points[cntOut],10,Scalar(0,0,180),1);
	//	
	//		
	//	}
	//	cout<<"minPt"<<cntIn<<"::"<<minDpt<<"___dist::"<<minD<<endl;
	// 	circle(imageC,(Point)minDpt,10,Scalar(0,0,180),4);
	//	minDistMatch.push_back(minDpt);
	//	putText(imageC,cv::format("%3.2f",minD),minDpt,CV_FONT_HERSHEY_COMPLEX ,2,Scalar(0,125,180));
	//line(imageC,(Point)contInner.points[cntIn],(Point)minDpt,Scalar(0,180,0),2);
	//}

	//putText(imageC,"XLP profile by AI",Point(80,120),CV_FONT_HERSHEY_COMPLEX,4,Scalar(0,0,255),2);

	cout << "contvalid :" << contValid << endl;
	//imwrite("resullt4core.bmp", imageC);
	/*
	for (int i=0;i<conts.size();i++)
	{
	drawContours(imageC,conts,i,Scalar(0,255,0),1);

	}*/
	imshow("draw", imageC);
	Mat res;
	resize(imageC, res, Size(), 0.3, 0.3);
	imshow("drawRes", res);
	waitKey();



	/* FileStorage fs("D:/CV/7sept vapi/algo/subPdata.txt", FileStorage::WRITE | FileStorage::FORMAT_YAML);
	 fs << "contours" << "[";
	 for (size_t i = 0; i < contours.size(); ++i)
	 {
		 fs << "{:";
		 fs << "points" << contours[i].points;
		 fs << "response" << contours[i].response;
		 fs << "direction" << contours[i].direction;
		 fs << "}";
	 }
	 fs << "]";
	 fs.release();
 */

	return 0;
}
