// algorithmLib.h

#pragma once
//#include <opencv2/imgcodecs.hpp>
//#include <opencv2/core/core.hpp>
//#include <opencv2/highgui/highgui.hpp>
//#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>
#include <iostream>
#include <stdio.h>
#include <string.h>
#include <vector>
#include <fstream>
#include <iterator>
#include <time.h>
using namespace System;
using namespace cv;
using namespace System::Runtime::InteropServices;
namespace algorithmLib {

	public ref class Algorithm
	{
	public:


		// TODO: Add your methods for this class here.

		System::Drawing::Bitmap^ algorithmLib::Algorithm::showGuides(System::Drawing::Bitmap^ bitmap0);

		void algorithmLib::Algorithm::vectorCheck();
		
		
		
		//void updateList(var item);
		//	int algorithmLib::Class1:: loadTemplate();
		static int outcode1 = 0;
		property int outCode1Prop
		{
			int get()
			{
				return outcode1;
			}
			void set(int  value)
			{
				outcode1 = value;
			};
		};

		static int outcode2 = 0;
		property int outCode2Prop
		{
			int get()
			{
				return outcode2;
			}
			void set(int  value)
			{
				outcode2 = value;
			};
		};


		static int outcode3 = 0;
		property int outCode3Prop
		{
			int get()
			{
				return outcode3;
			}
			void set(int  value)
			{
				outcode3 = value;
			};
		};


		static int inThreshold = 120;
		property int inThresholdProp
		{
			int get()
			{
				return inThreshold;
			}
			void set(int  value)
			{
				inThreshold = value;
			};
		};
	};
}
//algo.hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh