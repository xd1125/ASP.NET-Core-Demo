using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDemoModels
{
    public class Cinema
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="请输入电影院名称")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入电影院位置")]
        public string Location { get; set; }
        [Required(ErrorMessage = "请输入电影院容纳人数")]
        public int Capacity { get; set; }
    }
}
