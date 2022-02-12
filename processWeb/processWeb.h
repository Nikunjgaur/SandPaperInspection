  // processWeb.h
// opencvwithcsharp_in_one_project.h

#pragma once
#include "opencv2/opencv.hpp"
#include "opencv2/imgproc.hpp"
#include "opencv2/highgui.hpp"
#include <list>
#include <vector>
#include <iostream>
using namespace std;
using namespace cv;
using namespace System;

namespace processWeb {

	public ref class Class1
	{

		public:
		
			int processWeb::Class1::loadData();
			System::Drawing::Bitmap^ processWeb::Class1::processAll(System::Drawing::Bitmap^  bmp1);
			System::Drawing::Bitmap^ processWeb::Class1::processAllBack(System::Drawing::Bitmap^  bmp1);
			System::Drawing::Bitmap^ processWeb::Class1::processOne(System::Drawing::Bitmap^  bmp1);
			System::Drawing::Bitmap^ processWeb::Class1::showGuides(System::Drawing::Bitmap^ bitmap0);
			System::Drawing::Bitmap^ processWeb::Class1::processAllFront(System::Drawing::Bitmap^  bmp1);
			System::Drawing::Bitmap^ processWeb::Class1::processAllFrontThick(System::Drawing::Bitmap^ bmp1, System::Drawing::Bitmap^ bmp2, System::Drawing::Bitmap^ bmp3);
			int Class1::output();
			System::Drawing::Point Class1::getTopLeftPoint(int sectorNum);
			System::Drawing::Point Class1::getBottomRightPoint(int sectorNum);
			Double Class1::getDefectArea(int sectorNum);
		// TODO: Add your methods for this class here.
			static bool fineGrit=false;
		property bool fineGritProp
		{
			bool get()
			{
			return fineGrit;
			}
			void set(bool value)
			{
			fineGrit=value;
			}
		};

			static double mmperPix=0.1660;
		property double mmperPixProp
		{
			double get()
			{
			return mmperPix;
			}
			void set(double value)
			{
			mmperPix=value;
			}
		};

		static double pixPermm=6.1;
		property double pixPermmProp
		{
			double get()
			{
			return pixPermm;
			}
			void set(double value)
			{
			pixPermm=value;
			}
		};
		static int defAreaVectorSize = 0;
		property int defAreaSizeProp
		{
			int get()
			{
				return defAreaVectorSize;
			}
			void set(int value)
			{
				defAreaVectorSize = value;
			}
		};
		static double jumboWidth=0;
		property double jumboWidthProp
		{
			double get()
			{
			return jumboWidth;
			}
			void set(double value)
			{
			jumboWidth=value;
			}
		};
		static double defectArea = 0;
		property double defectAreaProp
		{
			double get()
			{
				return defectArea;
			}
			void set(double value)
			{
				defectArea = value;
			}
		};
		//edge trimming
		static int edgeTrim=5;
		property int edgeTrimProp
		{
			int get()
			{
			return edgeTrim;
			}
			void set(int value)
			{
			edgeTrim=value;
			}
		};
		static int defectCount = 0;
		property int defectCountProp
		{
			int get()
			{
				return defectCount;
			}
			void set(int value)
			{
				defectCount = value;
			}
		};

		static int defBlockSize = 0;
		property int defBlockSizeProp
		{
			int get()
			{
				return defBlockSize;
			}
			void set(int value)
			{
				defBlockSize = value;
			}
		};

		static System::String^ processWeb::Class1::path = "";
		property string pathProp
		{
			string get()
			{
				return pathProp;
			}
			void set(string value)
			{
				pathProp = value;
			}
		};

		static int sheetsInaRow=6;
		property int sheetsInaRowProp
		{
			int get()
			{
			return sheetsInaRow;
			}
			void set(int value)
			{
			sheetsInaRow=value;
			}
		};
		//-----------overlap settingVars
		static int overlapC1C2=86;
		property int overlapC1C2Prop
		{
			int get()
			{
			return overlapC1C2;
			}
			void set(int value)
			{
			overlapC1C2=value;
			}
		};
			static int overlapC2C3=50;
		property int overlapC2C3Prop
		{
			int get()
			{
			return overlapC2C3;
			}
			void set(int value)
			{
			overlapC2C3=value;
			}
		};
		//------------processing function thresholds-----------------
		static int th1=72;//72;
		property int th1Prop
		{
				int get()
			{
			return th1;
			}
			void set(int value)
			{
			th1=value;
			}
		}
		static int th2=26;//15;
		property int th2Prop
		{
				int get()
			{
			return th2;
			}
			void set(int value)
			{
			th2=value;
			}
		}
		static int th3=250;
		property int th3Prop
		{
				int get()
			{
			return th3;
			}
			void set(int value)
			{
			th3=value;
			}
		}
		static int th4=14000;
		property int th4Prop
		{
				int get()
			{
			return th4;
			}
			void set(int value)
			{
			th4=value;
			}
		}
		static int th5=10;
		property int th5Prop
		{
				int get()
			{
			return th5;
			}
			void set(int value)
			{
			th5=value;
			}
		}

		static int minAr=0;
		property int minArProp
		{
				int get()
			{
			return minAr;
			}
			void set(int value)
			{
			minAr=value;
			}
		}

		static int maxAr=10000000;
		property int maxArProp
		{
				int get()
			{
			return maxAr;
			}
			void set(int value)
			{
			maxAr=value;
			}
		}
		//------------------------------
		static bool trainTEST=false;
		property bool trainTESTProp
		{
			bool get()
			{
			return trainTEST;
			}
			void set(bool value)
			{
			trainTEST=value;
			}
		};

			static double sheetStartWidth=100;
		property double sheetStartWidthProp
		{
			double get()
			{
			return sheetStartWidth;
			}
			void set(double value)
			{
			sheetStartWidth=value;
			}
		};

		static int thresholdV=40;
		property int thresholdVProp
		{
			int get()
			{
			return thresholdV;
			}
			void set(int value)
			{
			thresholdV=value;
			}
		};



			static int sheetWidth=870;
		property int sheetWidthProp
		{
			int get()
			{
			return sheetWidth;
			}
			void set(int value)
			{
			sheetWidth=value;
			}
		};


		static int out1=1;
		property int out1Prop
		{
			int get()
			{
			return out1;
			}
			void set(int value)
			{
			out1=value;
			}
		};

				static int out2=1;
		property int out2Prop
		{
			int get()
			{
			return out2;
			}
			void set(int value)
			{
			out2=value;
			}
		};

				static int out3=1;
		property int out3Prop
		{
			int get()
			{
			return out3;
			}
			void set(int value)
			{
			out3=value;
			}
		};

				static int out4=1;
		property int out4Prop
		{
			int get()
			{
			return out4;
			}
			void set(int value)
			{
			out4=value;
			}
		};
				static int out5=1;
		property int out5Prop
		{
			int get()
			{
			return out5;
			}
			void set(int value)
			{
			out5=value;
			}
		};
		static int out6=1;
		property int out6Prop
		{
			int get()
			{
			return out6;
			}
			void set(int value)
			{
			out6=value;
			}
		};

				static int out7=1;
		property int out7Prop
		{
			int get()
			{
			return out7;
			}
			void set(int value)
			{
			out7=value;
			}
		};





	};
}
