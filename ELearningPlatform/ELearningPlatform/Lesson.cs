﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ELearningPlatform
{
    internal class Lesson
    {
        public string LessonTitle { get; }
        public string VideoPath { get; }
        public int VideoLength { get; }
        public double Rating { get; }
        public Teacher PrepareBy { get; }

        Timer timer;
        private int currState = 0;
        public bool IsPlaying = false;
        public Lesson(string lessonTitle,string videoPath,int videoLength, Teacher prepareBy)
        {
            LessonTitle = lessonTitle;
            VideoPath = videoPath;
            VideoLength = videoLength;
            PrepareBy = prepareBy;
        }
        public void ShowDetails()
        {
            Console.WriteLine($"{LessonTitle,-12}"+ "\t\t: "+VideoLength+"s");
        }

        public void Play()
        {
            if (IsPlaying)
            {
                Console.WriteLine("Already Playing");
            }
            else
            {
                IsPlaying = true;
                Console.WriteLine("The Duration is :"+VideoLength);
                timer = new Timer(TimerCallBack, null, currState, 1000);
            }
        }
        private void TimerCallBack(object o)
        {
            if (VideoLength > currState)
            {
                Console.WriteLine($"Video is playing {++currState}");
                GC.Collect();
            }
            else
            {
                Pause();
                
                
            }
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                Console.WriteLine("Video Pause!");
                IsPlaying = false;
                timer.Dispose();
            }
            else
            {
                Console.WriteLine("Video Not playing");
            }
        }

    }
}
