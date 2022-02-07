
// This is the main DLL file.

#include "stdafx.h"
#include "opencv2/core/utility.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include "opencv2/highgui/highgui.hpp"
#include "opencv2/photo/photo.hpp"
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>
#include "opencv2/opencv.hpp"
#include "opencv2/imgproc.hpp"
#include "opencv2/highgui.hpp"
#include "opencv2/core/core.hpp"
#include "opencv2/imgproc.hpp"
#include "opencv2/imgcodecs.hpp"
#include "opencv2/highgui.hpp"
#include "opencv2/opencv.hpp"
#include "algorithmLib.h"
#include "EdgesSubPix.h"
#include <vector>
using namespace System;
using namespace cv;
using namespace std;

typedef struct ThreshProperty
{
	string threshName;
	float threshValue;
};

int frameCount = 0;

int imgCount = 0;

Mat BitmapToMat(System::Drawing::Bitmap^ bitmap)
{
	cout << "B2M Enter " << endl;
	System::Drawing::Rectangle blank = System::Drawing::Rectangle(0, 0, bitmap->Width, bitmap->Height);
	System::Drawing::Imaging::BitmapData^ bmpdata = bitmap->LockBits(blank, System::Drawing::Imaging::ImageLockMode::ReadWrite, bitmap->PixelFormat);
	if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format8bppIndexed)
	{
		cout << "B2M Enter Format8bppIndexed " << endl;

		//tmp = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_8U, 1);
		int wb = ((bitmap->Width * 24 + 31) / 32) * 4;
		/*cv::Mat cv_img(cv::Size(bitmap->Width, bitmap->Height),
			CV_8UC3, bmpdata->Scan0.ToPointer(), wb);*/

		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC3, bmpdata->Scan0.ToPointer(), wb);// cv::Mat::AUT
		//tmp->imageData = (char*)bmData->Scan0.ToPointer();
		//cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC1, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;
	}

	else if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format32bppRgb)
	{
		cout << "B2M Enter Format32bppRgb " << endl;

		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC3, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;

	}

	Mat returnMat = (Mat::zeros(640, 480, CV_8UC1));
	//   bitmap->UnlockBits(bmData);
	return returnMat;
	//   return cvarrToMat(tmp);
}

Mat ImageCounter(Mat inImg) {

	Mat img;

	if (!inImg.empty())
	{
		img = inImg.clone();
		imshow("Im Img", inImg);
		putText(img, " NG", Point(4000, 150), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
		cout << img.rows << endl;

	}
	else
	{
		return img;
	}
	return img;
}

void VectorPrint() {
	struct ThreshProperty threshProp;

	threshProp.threshName = "Name is This";
	threshProp.threshValue = 45;

	vector<ThreshProperty> threshList;
	threshList.push_back(threshProp);
	cout << threshList.at(0).threshName << endl;
}


Mat GlassInspection(Mat img, int* reslt_p)
{

	try
	{
		Mat retImg;

		cvtColor(img, retImg, COLOR_BGR2GRAY);
		imshow("Gray Image", retImg);
		//threshold(retImg, retImg, 70, 250, THRESH_BINARY);
		cout << "Glass Inspected" << endl;
		*reslt_p = 0;
		return	retImg;

		
	}
	catch (exception ex)
	{
		*reslt_p = 1;
		putText(img, " NG", Point(100, 500), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
	}
}
//----------------------------------------new code-----small model-------------------------

System::Drawing::Bitmap^ algorithmLib::Algorithm::showGuides(System::Drawing::Bitmap^ bitmap0)
{

	Mat region = BitmapToMat(bitmap0);
	try {
		cout << "small run camera 1::::::::::::::" << region.channels() << endl;

		//putText(region, " OK...", Point(100,200), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
		int code = 1;
		//clock_t tStart1 = clock();
		Mat returnImg = ImageCounter(region);

		

		

		//cout << "time taken111111111111111111111111111111111111111111::::::::" << double(clock() - tStart1) / CLOCKS_PER_SEC << endl;


		
		//cout << "1922" << endl;
		/*if (code == 1)
		{
			putText(region, " NG", Point(500, 100), FONT_HERSHEY_DUPLEX, 5, Scalar(0, 0, 255), 5);
			outCode1Prop = 0;
		}
		else
		{
			outCode1Prop = 1;
			putText(region, " OK", Point(500, 100), FONT_HERSHEY_DUPLEX, 5, Scalar(0, 250, 0), 5);
		}*/

		return bitmap0;
	}
	catch (exception ex)
	{
		putText(region, " Exception", Point(100, 500), FONT_HERSHEY_DUPLEX, 10, Scalar(0, 0, 255), 6);
		outCode1Prop = 0;
		return bitmap0;
		//  System::Drawing::Bitmap^ dst=MatToBitmap(rotate_return)
	}

}

//algo

void algorithmLib::Algorithm::vectorCheck()
{
	VectorPrint();
}
