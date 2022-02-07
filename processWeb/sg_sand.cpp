
#include <iostream>
#include "opencv2/imgproc.hpp"
#include "opencv2/highgui.hpp"
#include <math.h>
#include <iostream>

using namespace cv;
using namespace std;


void resShow(string name, Mat img, float scale)
{
	Mat res;
	//scale = 0.2;
	resize(img, res, Size(), scale, scale);
	imshow(name, res);
	waitKey(5);
}
void Morph(const cv::Mat& src, cv::Mat& dst, int operation, int kernel_type, int size,int iterCnt =1)
{
	cv::Point anchor = cv::Point(size, size);
	cv::Mat element = getStructuringElement(kernel_type, cv::Size(2 * size + 1, 2 * size + 1), anchor);
	morphologyEx(src, dst, operation, element, anchor, iterCnt);
}

Mat getSobel(Mat imgGrey, int dx, int dy, int filterSz)
{
	Mat grad_x, grad_y;
	Mat abs_grad_x, abs_grad_y;
	Mat grad;
	//----edge detector
	/// Gradient X
	//Scharr( src_gray, grad_x, ddepth, 1, 0, scale, delta, BORDER_DEFAULT );
	Sobel(imgGrey, grad_x, CV_16S, dx, 0, filterSz, 1, 0, BORDER_DEFAULT);
	convertScaleAbs(grad_x, abs_grad_x);

	/// Gradient Y
	//Scharr( src_gray, grad_y, ddepth, 0, 1, scale, delta, BORDER_DEFAULT );
	Sobel(imgGrey, grad_y, CV_16S, 0, dy, filterSz, 1, 0, BORDER_DEFAULT);
	convertScaleAbs(grad_y, abs_grad_y);

	/// Total Gradient (approximate)
	addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, grad);

	imshow("grad", grad);

	return grad;
}

double par_minAr = 20;
int par_minSize = 20;
Mat processSandSG(Mat image)
{
	Mat drawImg;
	cvtColor(image, drawImg, COLOR_GRAY2BGR);
	Mat cannyRes;
	//cannyRes = getSobel(image,1,1, 3);
	adaptiveThreshold(image, cannyRes,255, ADAPTIVE_THRESH_MEAN_C,THRESH_BINARY_INV,121,12);
	Morph(cannyRes, cannyRes, MORPH_ERODE, MORPH_ELLIPSE, 1,1);
	Morph(cannyRes, cannyRes, MORPH_DILATE, MORPH_ELLIPSE, 3, 1);
	//----- finalDefects
	Mat contImgIn = cannyRes.clone();
	vector< vector<Point> > contours;
	vector<Vec4i> hierarchy;
	findContours(contImgIn, contours, hierarchy, RETR_EXTERNAL, CHAIN_APPROX_NONE);

	RotatedRect rr;
	double contAr;
	for (int i = 0; i < (int)contours.size(); i++)
	{
		contAr = contourArea(contours[i]);
		if (contAr > par_minAr)
		{
			rr = minAreaRect(contours[i]);

			//approxPolyDP(Mat(contours[i]), contours_poly[i], 3, true);
			if (rr.size.height>par_minSize || rr.size.width>par_minSize)
			{//drawContours(binary1,contours,i,0,-1);
				//drawContours(defV, contours, i, 255, -1);
				rectangle(drawImg, rr.boundingRect(), Scalar(0, 0, 100), 1,LINE_4);
				drawContours(drawImg, contours, i, Scalar(0, 0, 255), 1);

			}
		}
	}
	//------------------
	
	//Canny(image, cannyRes,100,200, 7);
	//resShow("canny", cannyRes,1);
	//drawImg.setTo(Scalar(0, 0, 200), cannyRes > 100);
	return drawImg;
}
int main()
{

	//Mat img = imread("D:/CV/bulb_filament/images/set1/4.bmp");
	//Mat img = imread("D:/CV/bulb_filament/0.bmp");
	//Mat img = imread("D:/CV/bulb_filament/b1.bmp");
	string basePath = "D:/CV/sg/defect/";
	
	//Mat tmpIn = imread(basePath + "template2.bmp", 0);
	Mat imageIn;
	for (int i = 100; i <=125; i++)
	{
	imageIn = imread(basePath + to_string(i)+".bmp",0);
	resize(imageIn, imageIn, Size(), 0.2, 0.2);
	Rect roi = Rect(0.12 * imageIn.cols, 0, imageIn.cols - (0.24 * imageIn.cols), imageIn.rows);
	imageIn = imageIn(roi);
	Mat detections = processSandSG(imageIn);
	resShow("defects", detections,1);
	resShow("imgRes", imageIn, 1); 
	waitKey();
	}
}
